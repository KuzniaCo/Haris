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
    unsigned long got_message;
    sensors.requestTemperatures();
    delay(500);
    sendViaRF(sensors.getTempCByIndex(0));
    if ( Serial.available() )
    {
      //Something when data from serial. For example adresss of cube
    }
}

void sendViaRF(float tempValue){
  if (!radio.write( &tempValue, sizeof(float) )){
    Serial.println(F("failed"));
  }
}