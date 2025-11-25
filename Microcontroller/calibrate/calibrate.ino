/*
1. Call set_scale() with no parameter.
2. Call tare() with no parameter.
3. Place a known weight on the scale and call get_units(10).
4. Divide the result in step 3 to your known weight. You should get about the parameter you need to pass to set_scale().
5. Adjust the parameter in step 4 until you get an accurate reading.
*/

#include "HX711.h"

const int LOADCELL_DOUT_PIN = 4;
const int LOADCELL_SCK_PIN = 2;
bool is_tared = false;
//const float CALIBRATION_FACTOR = 2;

HX711 scale;

void setup() {
  Serial.begin(57600);
  Serial.println("Initializing scale...");
  
  scale.begin(LOADCELL_DOUT_PIN, LOADCELL_SCK_PIN);

  // Tare the scale the first time it starts up
  scale.tare();
  
  // Set the flag to true to prevent retaring in the loop
  is_tared = true; 
  
  Serial.println("Scale is ready. Tared to zero.");
}
void loop() {
  delay(5000);
  scale.set_scale();
  Serial.println("Place weight");
  delay(5000);
  double units = scale.get_units(10);
  double result = units/100;
  Serial.println(result);
}