// Coordinates.h

#ifndef _COORDINATES_h
#define _COORDINATES_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

class CoordinatesClass
{
 protected:


 public:
	float Mouth = 0;
	float Roate = 0;
	float Coords[3];
	void init();

	void ConvertInput(String Input);

private :
	String getValue(String data, int index);
	
};

extern CoordinatesClass Coordinates;

#endif

