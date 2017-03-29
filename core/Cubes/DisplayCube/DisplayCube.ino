#include <SPI.h>
#include "RF24.h"
#include <EEPROM.h>
#include <Wire.h>   // standardowa biblioteka Arduino
#include <LiquidCrystal_I2C.h> // dolaczenie pobranej biblioteki I2C dla LCD

LiquidCrystal_I2C lcd(0x27, 2, 1, 0, 4, 5, 6, 7, 3, POSITIVE);  // Ustawienie adresu ukladu na 0x27

int PAYLOAD_SIZE = 30;
String CUBE_ADDRESS = "";
byte CUBE_ADDRESS_BYTES[6] = "";
RF24 radio(9, 10);

void getCubeAddress() {
	//Mocked addresses
	CUBE_ADDRESS = "00000";
	CUBE_ADDRESS.getBytes(CUBE_ADDRESS_BYTES, 6);
}

void setup() {
	Serial.begin(115200);
	Serial.println(F("HARIS/CUBE/Relay"));
  getCubeAddress();
	radio.begin();
	radio.setPALevel(RF24_PA_LOW);
	radio.openReadingPipe(1, CUBE_ADDRESS_BYTES);
	radio.startListening();
}

void loop() {
	char raw_message[30];
	if (radio.available()) {
		while (radio.available()) {
			radio.read(&raw_message, 30);
		}
		//String message(raw_message);
		Serial.println(raw_message);
    lcd.begin(16,2);   // Inicjalizacja LCD 2x16
  
    lcd.backlight(); // zalaczenie podwietlenia 
    lcd.setCursor(0,0); // Ustawienie kursora w pozycji 0,0 (pierwszy wiersz, pierwsza kolumna)
    lcd.print(raw_message);   
	}

}

//void sendViaRf(String message) {
//	radio.stopListening();
//	byte rawMessage[30];
//	message.getBytes(rawMessage, 30);
//	if (!radio.write(&rawMessage, 30)) {
//		Serial.println(F("failed send "));
//	}
//	radio.startListening();
//}
