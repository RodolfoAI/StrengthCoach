#include "HX711.h"

const int LOADCELL_DOUT_PIN = 4;
const int LOADCELL_SCK_PIN = 2;
const float CALIBRATION_FACTOR = 2.2;

HX711 scale;

double last_weight = 0.0;
int command = 0;

void setup() {
  Serial.begin(921600);
  Serial.println("Initializing scale...");
  
  scale.begin(LOADCELL_DOUT_PIN, LOADCELL_SCK_PIN);
  scale.set_scale(CALIBRATION_FACTOR); 
  scale.tare();  
  Serial.println("Scale is ready. Tared to zero.");
  last_weight = 300.0;
}
void loop() {
  Serial.println(scale.get_units());
  /*if (Serial.available() > 0)
  {
    command = Serial.read();
    
    // Check if the command is '1' to start the timed weighing
    if (command == '1') {
      // Record the time when the '1' command is received
      unsigned long start_time = millis(); 
      // Set the command back to '1' in case it was already read from Serial
      command = '1'; 

      // Loop while command is '1' AND less than 5000 milliseconds (5 seconds) have passed
      while (command == '1') { 
        unsigned long start_while_time = millis(); 
        // Get the current weight reading
        double current_weight = scale.get_units();
        Serial.println(current_weight);
        // Check if the current weight is greater than the last recorded weight
        if (current_weight > last_weight) {
          // Update the last_weight to the new higher weight
          last_weight = current_weight;
        }
        
        // This 'if' block will execute once the 5 seconds are up
        if ((millis() - start_time) >= 1000) {
          // Print highest weight registered in the time frame
          Serial.println(last_weight);
          last_weight = 200;
          // Set command to '0' to exit the loop
          command = '0';
        }
        //Serial.println(millis() - start_while_time);
      }
    }
  }*/
}