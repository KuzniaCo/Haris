#include <SPI.h>
#include "RF24.h"
#include <EEPROM.h>
#include <OneWire.h>
#include <DallasTemperature.h>
#include <Sleep_n0m1.h>

Sleep sleep;
unsigned long sleepTime;

int TEMP_DATA_PIN = 6;
String CUBE_ADDRESS = "aDs6g";
byte CUBE_ADDRESS_BYTES[6] = "";
String ENDPOINT_ADDRESS = "00000";
byte ENDPOINT_ADDRESS_BYTES[6] = "";
RF24 radio(9,10);

OneWire oneWire(TEMP_DATA_PIN);

DallasTemperature sensors(&oneWire);

void setup() {
  CUBE_ADDRESS.getBytes(CUBE_ADDRESS_BYTES, 6);
  ENDPOINT_ADDRESS.getBytes(ENDPOINT_ADDRESS_BYTES, 6);
  
  Serial.begin(115200);
  sleepTime = 1000;
  radio.begin();
  radio.setPALevel(RF24_PA_LOW);
  radio.openWritingPipe(ENDPOINT_ADDRESS_BYTES);

  sensors.begin();
}

void loop() {
    sensors.requestTemperatures();
    //sendViaRF(messageConstructor(String(sensors.getTempCByIndex(0))));
    Serial.println(messageConstructor(String(sensors.getTempCByIndex(0))));
    delay(500);
    radio.powerDown();
    sleep.pwrDownMode(); //set sleep mode
    sleep.sleepDelay(sleepTime); //sleep for: sleepTime
    radio.powerUp();
    delay(500);
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
  String message = CUBE_ADDRESS + separator + content + separator;

  return message;
}

