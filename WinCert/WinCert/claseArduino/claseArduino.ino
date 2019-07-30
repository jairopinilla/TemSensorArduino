 #include "max6675.h"
 #include <ArduinoJson.h>  

int ktcSO1 = 13;
int ktcCS1 = 12;
int ktcCLK1 = 11;

int ktcSO2 = 10;
int ktcCS2 = 9;
int ktcCLK2 = 8;

int ktcSO3 = 6;
int ktcCS3 = 5;
int ktcCLK3 = 4;

MAX6675 ktc2(ktcCLK2, ktcCS2, ktcSO2);
MAX6675 ktc3(ktcCLK3, ktcCS3, ktcSO3);
MAX6675 ktc1(ktcCLK1, ktcCS1, ktcSO1);

  
void setup() {
  Serial.begin(9600);
  // give the MAX a little time to settle
  delay(500);
}

void loop() {
   // basic readout test
  StaticJsonDocument<200> doc;

  doc["temp1"] = ktc1.readCelsius();
  doc["temp2"] = ktc2.readCelsius();
  doc["temp3"] = ktc3.readCelsius();

 // Serial.println(ktc1.readCelsius());
 // Serial.println(ktc2.readCelsius());
//  Serial.println(ktc3.readCelsius()); 
   
  serializeJson(doc, Serial);
  Serial.println("");

 // Serial.println(ktc1.readCelsius());
  delay(1000);
}
