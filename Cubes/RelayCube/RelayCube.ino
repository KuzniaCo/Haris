#include <SPI.h>
#include "RF24.h"
#include <EEPROM.h>

int PAYLOAD_SIZE = 30;
int RELAY_PIN = 6;
String CUBE_ADDRESS = "";
byte CUBE_ADDRESS_BYTES[6] = "";
byte RELAY_PIN_STATUS = 0;
RF24 radio(9,10);
byte addresses[][6] = {"Gate"};

void getCubeAddress() {
  // Zapytanie pamięci w przeciwnym wypadku wysłanie wiadomości powitalnej

  //Mocked addresses
   CUBE_ADDRESS="aPd12";
   CUBE_ADDRESS.getBytes(CUBE_ADDRESS_BYTES, 6);
}

void setup() {
  Serial.begin(115200);
  Serial.println(F("HARIS/CUBE/Relay"));

  byte memoryValue = EEPROM.read(0);
  if(memoryValue != 255){
    RELAY_PIN_STATUS = memoryValue;
  }

  pinMode(RELAY_PIN, OUTPUT);
  digitalWrite(RELAY_PIN, RELAY_PIN_STATUS);

  radio.begin();
  radio.setPALevel(RF24_PA_LOW);
  radio.openReadingPipe(1,CUBE_ADDRESS_BYTES);
  radio.startListening();
}

void loop() {
    char raw_message[30];

    if (radio.available()){
      while (radio.available()) {
        radio.read( &raw_message,  PAYLOAD_SIZE);
      }
      String message(raw_message);
      int action = messageDecode(message).toInt();
      
      if(action == 1){
        RELAY_PIN_STATUS = 1;
        digitalWrite(RELAY_PIN, RELAY_PIN_STATUS);
        EEPROM.write(0, RELAY_PIN_STATUS);
      }
      else if(action == 0){
        RELAY_PIN_STATUS = 0;
        digitalWrite(RELAY_PIN, RELAY_PIN_STATUS);
        EEPROM.write(0, RELAY_PIN_STATUS);
      }
   }

  if ( Serial.available() )
  {
  }
}

String messageDecode(String message) {
  onPing(message);
  return message.substring(7, 8);
}

void onPing(String message){
  if(message.substring(1, 6) == "ping"){
    radio.stopListening();
    String pongMsg = messageConstructor("pong");
    sendViaRf(pongMsg);
  }
}

void sendViaRf(String message){
  radio.stopListening();
  byte rawMessage[30];
  message.getBytes(rawMessage, 30);
  if (!radio.write( &rawMessage, 30 )){
    Serial.println(F("failed send "));
  }
  radio.startListening();
}

String messageConstructor(String content){
  String separator = "|";
  String message = CUBE_ADDRESS + separator + content;

  return message;
}

