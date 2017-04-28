#include <Ethernet.h>
#include <SPI.h>
#include "RF24.h"
#include <EEPROM.h>

byte mac[] = { 0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED };
byte server[] = { 193, 70, 84, 40 }; // Local 193.70.84.40

EthernetClient client;


int PAYLOAD_SIZE = 30;
String CUBE_ADDRESS = "";
byte CUBE_ADDRESS_BYTES[6] = "";
RF24 radio(7, 6);

void getCubeAddress() {
  //Mocked addresses
  CUBE_ADDRESS = "00000";
  CUBE_ADDRESS.getBytes(CUBE_ADDRESS_BYTES, 6);
}

void setup()
{
  Ethernet.begin(mac);
  Serial.begin(115200);
  Serial.println(F("HARIS/CUBE/SocketEndpoint"));
  getCubeAddress();
  radio.begin();
  radio.setPALevel(RF24_PA_LOW);
  radio.openReadingPipe(1, CUBE_ADDRESS_BYTES);
  radio.openWritingPipe(CUBE_ADDRESS_BYTES);
  radio.startListening();  

  delay(1000);
  connect();
}

void loop()
{
  char raw_message[30];
  if (radio.available()) {
    while (radio.available()) {
      radio.read(&raw_message, 30);
    }
    Serial.println(raw_message);
    client.print(raw_message);
  }
  if (client.available()) {
    String message="";
    while(client.available()){
      char recived = client.read();
      message.concat(String(recived));
    }
    Serial.println(message);
    sendViaRF(message);
  }
  if (!client.connected()) {
    Serial.println();
    Serial.println("disconnecting.");
    client.stop();
    Serial.println("try connect again.");
    connect();
    }
}

void connect(){
  Serial.println("connecting...");

  if (client.connect(server, 11000)) {
    Serial.println("connected");
    client.print("aDs6g|12.2|");
  } else {
    Serial.println("connection failed");
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

void sendViaRF(String message){
  String address = getValue(message, '|', 0);
  byte addressBytes[6] = "";
  address.getBytes(addressBytes, 6);
  radio.openWritingPipe(addressBytes);
  
  radio.stopListening();
  char charArray[30];
  message.toCharArray(charArray, 30);

  bool isSend = false;
  int counter = 0;

  while(!isSend){
    if (radio.write(&charArray, 30)) {
      isSend = true;
    }
    else{
      Serial.println(F("failed send "));
    }
    delay(1000);   
    counter++;
    if(counter >5) {
      break; 
    }
  }
  radio.startListening();
}
