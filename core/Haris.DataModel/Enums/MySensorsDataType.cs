namespace Haris.DataModel.Enums
{
	internal enum MySensorsDataType
	{
		V_TEMP, // S_TEMP. Temperature S_TEMP, S_HEATER, S_HVAC
		V_HUM, // S_HUM. Humidity
		V_STATUS,
		//  S_LIGHT, S_DIMMER, S_SPRINKLER, S_HVAC, S_HEATER. Used for setting/reporting binary (on/off) status. 1=on, 0=off  
		V_LIGHT, // Same as V_STATUS
		V_PERCENTAGE, // S_DIMMER. Used for sending a percentage value 0-100 (%). 
		V_DIMMER, // S_DIMMER. Same as V_PERCENTAGE.  
		V_PRESSURE, // S_BARO. Atmospheric Pressure
		V_FORECAST,
		// S_BARO. Whether forecast. string of "stable", "sunny", "cloudy", "unstable", "thunderstorm" or "unknown"
		V_RAIN, // S_RAIN. Amount of rain
		V_RAINRATE, // S_RAIN. Rate of rain
		V_WIND, // S_WIND. Wind speed
		V_GUST, // S_WIND. Gust
		V_DIRECTION, // S_WIND. Wind direction 0-360 (degrees)
		V_UV, // S_UV. UV light level
		V_WEIGHT, // S_WEIGHT. Weight(for scales etc)
		V_DISTANCE, // S_DISTANCE. Distance
		V_IMPEDANCE, // S_MULTIMETER, S_WEIGHT. Impedance value
		V_ARMED, // S_DOOR, S_MOTION, S_SMOKE, S_SPRINKLER. Armed status of a security sensor. 1 = Armed, 0 = Bypassed
		V_TRIPPED,
		// S_DOOR, S_MOTION, S_SMOKE, S_SPRINKLER, S_WATER_LEAK, S_SOUND, S_VIBRATION, S_MOISTURE. Tripped status of a security sensor. 1 = Tripped, 0
		V_WATT, // S_POWER, S_LIGHT, S_DIMMER, S_RGB, S_RGBW. Watt value for power meters
		V_KWH, // S_POWER. Accumulated number of KWH for a power meter
		V_SCENE_ON, // S_SCENE_CONTROLLER. Turn on a scene
		V_SCENE_OFF, // S_SCENE_CONTROLLER. Turn of a scene
		V_HEATER, // Deprecated. Use V_HVAC_FLOW_STATE instead.
		V_HVAC_FLOW_STATE, // S_HEATER, S_HVAC. HVAC flow state ("Off", "HeatOn", "CoolOn", or "AutoChangeOver") 
		V_HVAC_SPEED, // S_HVAC, S_HEATER. HVAC/Heater fan speed ("Min", "Normal", "Max", "Auto") 
		V_LIGHT_LEVEL, // S_LIGHT_LEVEL. Uncalibrated light level. 0-100%. Use V_LEVEL for light level in lux
		V_VAR1,
		V_VAR2,
		V_VAR3,
		V_VAR4,
		V_VAR5,
		V_UP, // S_COVER. Window covering. Up
		V_DOWN, // S_COVER. Window covering. Down
		V_STOP, // S_COVER. Window covering. Stop
		V_IR_SEND, // S_IR. Send out an IR-command
		V_IR_RECEIVE, // S_IR. This message contains a received IR-command
		V_FLOW, // S_WATER. Flow of water (in meter)
		V_VOLUME, // S_WATER. Water volume
		V_LOCK_STATUS, // S_LOCK. Set or get lock status. 1=Locked, 0=Unlocked
		V_LEVEL, // S_DUST, S_AIR_QUALITY, S_SOUND (dB), S_VIBRATION (hz), S_LIGHT_LEVEL (lux)
		V_VOLTAGE, // S_MULTIMETER 
		V_CURRENT, // S_MULTIMETER
		V_RGB, // S_RGB_LIGHT, S_COLOR_SENSOR. 
		// Used for sending color information for multi color LED lighting or color sensors. 
		// Sent as ASCII hex: RRGGBB (RR=red, GG=green, BB=blue component)
		V_RGBW, // S_RGBW_LIGHT
		// Used for sending color information to multi color LED lighting. 
		// Sent as ASCII hex: RRGGBBWW (WW=white component)
		V_ID, // S_TEMP
		// Used for sending in sensors hardware ids (i.e. OneWire DS1820b). 
		V_UNIT_PREFIX, // S_DUST, S_AIR_QUALITY
		// Allows sensors to send in a string representing the 
		// unit prefix to be displayed in GUI, not parsed by controller! E.g. cm, m, km, inch.
		// Can be used for S_DISTANCE or gas concentration
		V_HVAC_SETPOINT_COOL, // S_HVAC. HVAC cool setpoint (Integer between 0-100)
		V_HVAC_SETPOINT_HEAT, // S_HEATER, S_HVAC. HVAC/Heater setpoint (Integer between 0-100)
		V_HVAC_FLOW_MODE, // S_HVAC. Flow mode for HVAC ("Auto", "ContinuousOn", "PeriodicOn")

	}
}