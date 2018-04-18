
#include <Braccio.h>
#include <Servo.h>
#include <SoftwareSerial.h>

SoftwareSerial mySerial(10, 11);
Servo base;
Servo shoulder;
Servo elbow;
Servo wrist_rot;
Servo wrist_ver;
Servo gripper;

void setup()
{
	Braccio.begin();

	mySerial.begin(115200);
	mySerial.println("Hello, world");
}

// the loop function runs over and over again forever
void loop()
{
	String Input;
	do 
	{
		Input=Serial.readString();
	} while (Input == "");

	Serial.println("got input: " + Input);
	if (Input == "test") {
		Braccio.ServoMovement(30, 90, 90, 90, 90, 90, 90);
	}

	// wait for a second
}
