#include "HX711.h"

#include "BLEDevice.h"
#include "BLEServer.h"
#include "BLEUtils.h"
#include "BLE2902.h"

const int LOADCELL_DOUT_PIN = 4;
const int LOADCELL_SCK_PIN  = 2;
const float CALIBRATION_FACTOR = 2.2;

HX711 scale;

double threshold_force = 300;
double highest_force   = 0;
double cycle_force     = 0;
int command = 0;


BLECharacteristic *pCharacteristic;
bool deviceConnected = false;
String rxload = "";

#define SERVICE_UUID           "6E400001-B5A3-F393-E0A9-E50E24DCCA9E" 
#define CHARACTERISTIC_UUID_RX "6E400002-B5A3-F393-E0A9-E50E24DCCA9E"
#define CHARACTERISTIC_UUID_TX "6E400003-B5A3-F393-E0A9-E50E24DCCA9E"

class MyServerCallbacks: public BLEServerCallbacks {
  void onConnect(BLEServer* pServer) {
    deviceConnected = true;
  };
  void onDisconnect(BLEServer* pServer) {
    deviceConnected = false;
  }
};

class MyCallbacks: public BLECharacteristicCallbacks {
  void onWrite(BLECharacteristic *pCharacteristic) {
    String rxValue = pCharacteristic->getValue();

    if (rxValue.length() > 0) {
      rxload = "";
      for (int i = 0; i < rxValue.length(); i++) {
        rxload += (char)rxValue[i];
      }
    }
  }
};

void setupBLE(String BLEName) {

  BLEDevice::init(BLEName.c_str());

  BLEServer *pServer = BLEDevice::createServer();
  pServer->setCallbacks(new MyServerCallbacks());

  BLEService *pService = pServer->createService(SERVICE_UUID);

  // TX (notify)
  pCharacteristic = pService->createCharacteristic(
                      CHARACTERISTIC_UUID_TX,
                      BLECharacteristic::PROPERTY_NOTIFY
                    );
  pCharacteristic->addDescriptor(new BLE2902());

  // RX (write)
  BLECharacteristic *pRxCharacteristic = pService->createCharacteristic(
                                           CHARACTERISTIC_UUID_RX,
                                           BLECharacteristic::PROPERTY_WRITE
                                         );
  pRxCharacteristic->setCallbacks(new MyCallbacks());

  pService->start();
  pServer->getAdvertising()->start();
}


void setup() {

  Serial.begin(115200);
  setupBLE("ESP32S3_Bluetooth");

  scale.begin(LOADCELL_DOUT_PIN, LOADCELL_SCK_PIN);
  scale.set_scale(CALIBRATION_FACTOR); 
  scale.tare();  

}


void loop() {

  if (rxload.length() > 0) {
    command = rxload[0];   
    rxload = "";
  }


  if (command == '1') {

    unsigned long start_time = millis(); 
    command = '1'; 

    while (command == '1') { 

      cycle_force = scale.get_units();

      if (cycle_force > highest_force) {
        highest_force = cycle_force;
      }

      if ((millis() - start_time) >= 4000) {

        if (highest_force > threshold_force) {

          if (deviceConnected) {
            String out = String(highest_force);
            pCharacteristic->setValue(out.c_str());
            pCharacteristic->notify();
          }

        } else {

          if (deviceConnected) {
            pCharacteristic->setValue("0");
            pCharacteristic->notify();
          }
        }

        command = '0';
        highest_force = 0;
        scale.tare();
      }
    }
  }
}
