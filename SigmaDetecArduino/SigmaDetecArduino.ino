// Arduino/Teensy example for Arduino Braccio
#undef PI
#undef HALF_PI
// Include the library InverseK.h

#include <Servo.h>
#include <Braccio.h>
#include <InverseK.h>
#include "Coordinates.h"


// docs on braccio
// 670 is length of braccio

Servo base;
Servo shoulder;
Servo elbow;
Servo wrist_rot;
Servo wrist_ver;
Servo gripper;

CoordinatesClass Movement;

float a0, a1, a2, a3, mouth;

void setup() {
	// Setup the lengths and rotation limits for each link
	Link baseh, upperarmh, forearmh, handh;
	Braccio.begin();
	
	baseh.init(0, b2a(0.0), b2a(180.0));
	upperarmh.init(200, b2a(15.0), b2a(165.0));
	forearmh.init(200, b2a(0.0), b2a(180.0));
	handh.init(270, b2a(0.0), b2a(180.0));
	Movement.init();
	// Attach the links to the inverse kinematic model
	InverseK.attach(baseh, upperarmh, forearmh, handh);
}


void loop() 
{
	//Move:100:200:300
	//Grabber:10
	//Rotate:10
	String Input="";

	Input = Serial.readString();
	
	if (Input != "")
	{
		Movement.ConvertInput(Input);
		if (!InverseK.solve(Movement.Coords[0], Movement.Coords[1], Movement.Coords[2], a0, a1, a2, a3))
		{
			Serial.println("failed");
		}
		else
		{
			Braccio.ServoMovement(30, a2b(a0), a2b(a1), a2b(a2), a2b(a3), Movement.Roate, Movement.Mouth);
			Serial.println("moving");
		}
	}
}

// Quick conversion from the Braccio angle system to radians 
float b2a(float b) {
	return b / 180.0 * PI - HALF_PI;
}

// Quick conversion from radians to the Braccio angle system
float a2b(float a) {
	return (a + HALF_PI) * 180 / PI;
}