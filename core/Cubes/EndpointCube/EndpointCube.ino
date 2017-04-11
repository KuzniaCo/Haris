#include <SPI.h>
#include "RF24.h"
#include <EEPROM.h>

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
//	Serial.println(F("HARIS/CUBE/Relay"));
//  getCubeAddress();
//	radio.begin();
//	radio.setPALevel(RF24_PA_LOW);
//	radio.openReadingPipe(1, CUBE_ADDRESS_BYTES);
//	radio.startListening();
}

void loop() {
//	char raw_message[30];
//	if (radio.available()) {
//		while (radio.available()) {
//			radio.read(&raw_message, 30);
//		}
//		//String message(raw_message);
//		Serial.println(raw_message);
//	}
Serial.println("aDs6g|13.2|");
delay(3000);

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
