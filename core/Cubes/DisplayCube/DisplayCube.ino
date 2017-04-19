//*  VCC - > 5 V
//*  GND - GND
//*  SCL -> A5
//*  SDA -> A4

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
	CUBE_ADDRESS = "00000";
	CUBE_ADDRESS.getBytes(CUBE_ADDRESS_BYTES, 6);
}

void setup() {
	Serial.begin(115200);
	//Serial.println(F("HARIS/CUBE/DISPLAY"));
  getCubeAddress();
	radio.begin();
	radio.setPALevel(RF24_PA_LOW);
	radio.openReadingPipe(1, CUBE_ADDRESS_BYTES);
	radio.startListening();
  lcd.begin(16,2);
}

void loop() {
	char raw_message[30];
	if (radio.available()) {
		while (radio.available()) {
			radio.read(&raw_message, 30);
		}
		String message(raw_message);
    decodeMessage(message);
		Serial.println(raw_message);
	}
  if (Serial.available())
  {
    String serialMsg = Serial.readString();
    decodeMessage(serialMsg);
    //Serial.println("Recived:"+serialMsg);
  }
}

void clearLine(int line){
   lcd.setCursor(0,line);
   lcd.print("                ");
}

void sendViaRF(String message){
  radio.stopListening();
  char charArray[30];
  message.toCharArray(charArray, 30);
  if (!radio.write( &charArray, 30 )){
    //Serial.println("failed send "+ message);
  }
  radio.startListening();
}

void decodeMessage(String message){
  String action = getValue(message, '|', 1);
  if(action == "1"){
    String row = getValue(message, '|', 2);
    String content = getValue(message, '|', 3);
    setDisplay(row, content);
    //Serial.println("Action: Set Display");
    //Serial.println("Row: " + row);
    //Serial.println("Content: " + content);
  }
  else if(action == "2"){
    String newState = getValue(message, '|', 2);
    setBacklight(newState);
    //Serial.println("Action: Backlight");
    //sendViaRF("OK");
  }
  else{
    //sendViaRF("ERROR");
    //Serial.println("Action: Error action");
  }
}

void setBacklight(String newState){
  if(newState == "0"){
    lcd.noBacklight();
  }
  else if(newState == "1"){
    lcd.backlight();    
  }
  else{
    //sendViaRF("ERROR");
  }
}

void setDisplay(String row, String content){
   int rowInt = row.toInt();
   clearLine(rowInt);
   lcd.setCursor(0,rowInt);
   lcd.print(content);
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

String messageConstructor(String content){
  String separator = "|";
  String message = CUBE_ADDRESS + separator + content;

  return message;
}
