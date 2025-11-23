#include "HX711.h"

const int LOADCELL_DOUT_PIN = 4;
const int LOADCELL_SCK_PIN = 2;
const float CALIBRATION_FACTOR = 2.2;

HX711 scale;

double threshold_force = 300;
double highest_force = 0;
double cycle_force = 0;
int command = 0;

void setup() {
  Serial.begin(9600);
  Serial.println("Initializing scale...");
  
  scale.begin(LOADCELL_DOUT_PIN, LOADCELL_SCK_PIN);
  scale.set_scale(CALIBRATION_FACTOR); 
  scale.tare();  
  Serial.println("Scale is ready. Tared to zero.");
}
void loop() {
  //Serial.println(scale.get_units());
  if (Serial.available() > 0)
  {
    command = Serial.read();
    
    if (command == '1') {
      unsigned long start_time = millis(); 
      // Set the command back to '1' in case it was already read from Serial
      command = '1'; 

      while (command == '1') { 
        cycle_force = scale.get_units();
        
        // Check if the current weight is greater than the last recorded weight
        if (cycle_force > highest_force) {
          highest_force = cycle_force;
        }
        
        if ((millis() - start_time) >= 6000) {

          if (highest_force > threshold_force) {
            Serial.println(highest_force);
          }
          else{
            Serial.println("0");  
          }
          // Set command to '0' to exit the loop
          command = '0';
          highest_force = 0;
          scale.tare();
        }
      }
    }
  }
}