#include <SPI.h>
#include "RF24.h"
#include <EEPROM.h>
#include <OneWire.h>
#include <DallasTemperature.h>

int TEMP_DATA_PIN = 6;

RF24 radio(9,10);

OneWire oneWire(TEMP_DATA_PIN);

DallasTemperature sensors(&oneWire);

byte addresses[][6] = {"Gate","Temp"};

void setup() {
  Serial.begin(115200);
  Serial.println(F("TempCube Start"));

  radio.begin();
  radio.setPALevel(RF24_PA_LOW);
  radio.openWritingPipe(addresses[0]);

  sensors.begin();
}

void loop() {
    sensors.requestTemperatures();
    delay(500);
    sendViaRF(constructMessage(sensors.getTempCByIndex(0)));

    if ( Serial.available() )
    {
      //Something when data from serial. For example adresss of cube
    }
}

void sendViaRF(String message){
  if (!radio.write( &message, sizeof(message) )){
    Serial.println(F("failed send "+ message));
  }
}

String constructMessage(float tempValue){
  float tempValue;
  String address = "adDs3"
  String message = address + tempValue;

  return message;
}
