#include "Coordinates.h"

void CoordinatesClass::init()
{
	

}

void CoordinatesClass::ConvertInput(String Input)
{
	const char* grabber = "Grabber";
	const char* move = "Move";
	const char* rotate = "Rotate";

	
	if (getValue(Input,0) == grabber)
	{
		this->Mouth = getValue(Input, 1).toFloat();
	}
	else if (getValue(Input, 0) == move)
	{
		for (int i = 1; i < 4; i++)
		{
			this->Coords[i-1] = getValue(Input, i).toFloat();
		}
	}
	else if (getValue(Input, 0) == rotate)
	{
		this->Roate = getValue(Input, 1).toFloat();
	}
}

String CoordinatesClass::getValue(String data, int index)
{
	{
		
		char separator = ':';
		int found = 0;
		int strIndex[] = { 0, -1 };
		int maxIndex = data.length() - 1;

		for (int i = 0; i <= maxIndex && found <= index; i++) {
			if (data.charAt(i) == separator || i == maxIndex) {
				found++;
				strIndex[0] = strIndex[1] + 1;
				strIndex[1] = (i == maxIndex) ? i + 1 : i;
			}
		}

		return found>index ? data.substring(strIndex[0], strIndex[1]) : "";
	}
}


CoordinatesClass Coordinates;

