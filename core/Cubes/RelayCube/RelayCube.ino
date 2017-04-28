#include <SPI.h>
#include "RF24.h"
#include <EEPROM.h>

int PAYLOAD_SIZE = 30;
int RELAY_PIN = 6;
String CUBE_ADDRESS = "";
byte RELAY_PIN_STATUS = 0;
RF24 radio(9, 10);
byte CUBE_ADDRESS_BYTES[6] = "";
String ENDPOINT_ADDRESS = "00000";
byte ENDPOINT_ADDRESS_BYTES[6] = "";
int cubeIsAddressed = 0;

bool getCubeAddress() {
  cubeIsAddressed = EEPROM.read(0);

  Serial.println(cubeIsAddressed);
  if(cubeIsAddressed == 1){
    EEPROM.get(1, CUBE_ADDRESS_BYTES);
     //get address from eeprom
    CUBE_ADDRESS = String((char *)CUBE_ADDRESS_BYTES);
    Serial.println(CUBE_ADDRESS);
    return true;
  }
  else{
    // random temporary address from ASCI code from a to z
    for(int i=0; i<5; i++){
      CUBE_ADDRESS_BYTES[i] = random(97, 122);
    }
    Serial.println(String((char *)CUBE_ADDRESS_BYTES));
    CUBE_ADDRESS = String((char *)CUBE_ADDRESS_BYTES);
    ENDPOINT_ADDRESS.getBytes(ENDPOINT_ADDRESS_BYTES, 6);
    return false;
  }
}

void sendHelloMessage(){
  sendViaRf("|"+CUBE_ADDRESS+"|RelayCube");
}

void setup() {
	Serial.begin(115200);
	Serial.println(F("HARIS/CUBE/Relay"));
  randomSeed(analogRead(0));
  
	byte memoryValue = EEPROM.read(10);
	if (memoryValue != 255) {
		RELAY_PIN_STATUS = memoryValue;
	}
  pinMode(RELAY_PIN, OUTPUT);
  digitalWrite(RELAY_PIN, RELAY_PIN_STATUS);
  radio.begin();
  radio.setPALevel(RF24_PA_LOW);
  if(getCubeAddress()){
    radio.openReadingPipe(1, CUBE_ADDRESS_BYTES);
    radio.startListening();
  }
  else {
    radio.openWritingPipe(ENDPOINT_ADDRESS_BYTES);
    radio.openReadingPipe(1, CUBE_ADDRESS_BYTES);
    sendHelloMessage();
    radio.startListening();
  }
}

void storeAddress(String address){
  CUBE_ADDRESS = address;
  CUBE_ADDRESS.getBytes(CUBE_ADDRESS_BYTES, 6);
  EEPROM.put(1, CUBE_ADDRESS_BYTES);
  EEPROM.write(0, 1);
  Serial.println("Zapisano address");
  cubeIsAddressed = 1;
  radio.stopListening();
  radio.openReadingPipe(1, CUBE_ADDRESS_BYTES);
  radio.startListening();
}

void loop() {

  if(cubeIsAddressed != 1){
    char raw_message[30];
  
    if (radio.available()) {
      while (radio.available()) {
        radio.read(&raw_message, PAYLOAD_SIZE);
      }
      String message(raw_message);
      Serial.println(message);
      String address = getValue(message, '|', 1);
      storeAddress(address);
    }   
  }
  else {
    char raw_message[30];
  
    if (radio.available()) {
      while (radio.available()) {
        radio.read(&raw_message, PAYLOAD_SIZE);
      }
      String message(raw_message);
      int action = getValue(message, '|', 1).toInt();
      Serial.println("Akcja: "+ String(action));
      if (action == 1) {
        RELAY_PIN_STATUS = 1;
        digitalWrite(RELAY_PIN, RELAY_PIN_STATUS);
        EEPROM.write(10, RELAY_PIN_STATUS);
      }
      else if (action == 0) {
        RELAY_PIN_STATUS = 0;
        digitalWrite(RELAY_PIN, RELAY_PIN_STATUS);
        EEPROM.write(10, RELAY_PIN_STATUS);
      }
    }
  }
  if(Serial.available()){
    String mess = Serial.readString();
    if(mess == "newAddress"){
      EEPROM.write(0, 0);
      Serial.println("Zapisano address");
      cubeIsAddressed = 0;      
    }
  }
}

String getValue(String data, char separator, int index)
{
  int found = 0;
  int strIndex[] = {0, -1};
  int maxIndex = data.length()-1;

  for(int i=0; i<=maxIndex && found<=index; i++){
    if(data.charAt(i)==separator || i==maxIndex){
        found++;
        strIndex[0] = strIndex[1]+1;
        strIndex[1] = (i == maxIndex) ? i+1 : i;
    }
  }

  return found>index ? data.substring(strIndex[0], strIndex[1]) : "";
}


String messageDecode(String message) {
	return message.substring(7, 8);
}

void sendViaRf(String message) {
	radio.stopListening();
	byte rawMessage[30];
	message.getBytes(rawMessage, 30);
  bool isSend = false;

  while(!isSend){
    if (radio.write(&rawMessage, 30)) {
      isSend = true;
    }
    else{
      Serial.println(F("failed send "));
    }
    
    delay(1000);    
  }

	radio.startListening();
}

String messageConstructor(String content) {
	String separator = "|";
	String message = CUBE_ADDRESS + separator + content;

	return message;
}
