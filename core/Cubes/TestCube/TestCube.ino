#include <SPI.h>
#include "RF24.h"
#include <EEPROM.h>

long randomNumber;

String CUBE_ADDRESS = "aDs2c";
byte CUBE_ADDRESS_BYTES[6] = "";
String ENDPOINT_ADDRESS = "00000";
byte ENDPOINT_ADDRESS_BYTES[6] = "";
RF24 radio(9,10);

void setup() {
  CUBE_ADDRESS.getBytes(CUBE_ADDRESS_BYTES, 6);
  ENDPOINT_ADDRESS.getBytes(ENDPOINT_ADDRESS_BYTES, 6);
  
  Serial.begin(115200);
  radio.begin();
  radio.setPALevel(RF24_PA_MAX);
  radio.openWritingPipe(ENDPOINT_ADDRESS_BYTES);
}

void loop() {
    randomNumber = random(1000);
    sendViaRF(messageConstructor(String(randomNumber)));
    Serial.println(messageConstructor(String(randomNumber)));
    delay(1000);
}

void sendViaRF(String message){
  char charArray[30];
  message.toCharArray(charArray, 30);
  if (!radio.write( &charArray, 30 )){
    Serial.println("failed send "+ message);
  }
}

String messageConstructor(String content){
  String separator = "|";
  String message = ENDPOINT_ADDRESS + separator + "1" + separator + "1" + separator + content;

  return message;
}

