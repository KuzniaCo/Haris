
/*
* Getting Started example sketch for nRF24L01+ radios
* This is a very basic example of how to send data from one node to another
* Updated: Dec 2014 by TMRh20
*/

#include <SPI.h>
#include "RF24.h"
#include <EEPROM.h>

int RELAY_PIN = 6;

byte RELAY_PIN_STATUS = 0;

RF24 radio(9,10);
byte addresses[][6] = {"Gate","Relay"};

void setup() {
  Serial.begin(115200);
  Serial.println(F("RF24/examples/GettingStarted"));

  byte memoryValue = EEPROM.read(0);
  if(memoryValue != 255){
    RELAY_PIN_STATUS = memoryValue;
  }
  
  pinMode(RELAY_PIN, OUTPUT);
  digitalWrite(RELAY_PIN, RELAY_PIN_STATUS);

  radio.begin();
  radio.setPALevel(RF24_PA_LOW);
  radio.openReadingPipe(1,addresses[1]);
  radio.startListening();
}

void loop() {
    unsigned long got_message;
    
    if (radio.available()){                                                               
      while (radio.available()) {                                   
        radio.read( &got_message, sizeof(unsigned long) );             
      }

      if(got_message == 11){
        RELAY_PIN_STATUS = 1;
        digitalWrite(RELAY_PIN, RELAY_PIN_STATUS);
        EEPROM.write(0, RELAY_PIN_STATUS);
      }
      else if(got_message == 10){
        RELAY_PIN_STATUS = 0;
        digitalWrite(RELAY_PIN, RELAY_PIN_STATUS);
        EEPROM.write(0, RELAY_PIN_STATUS);
      }
   }

  if ( Serial.available() )
  {
  }
}
