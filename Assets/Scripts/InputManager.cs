﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// A custom input manager because I can't stand the unity default. This code will not be documented because it is mostly self-explanatory
public class InputManager : MonoBehaviour {

	public static InputManager instance;

	public enum Button{FIRE,GEAR,CANOPY,EJECT,TOGGLE_WEAPONS,WEAPON_LEFT,WEAPON_RIGHT,FIRE_CANNON};
	static string[] joyButtons = {"joystick button 1", "joystick button 3","joystick button 6","joystick button 7","joystick button 2","joystick button 4","joystick button 5","joystick button 0"};
	static KeyCode[] keyButtons = {KeyCode.Space, KeyCode.G, KeyCode.C, KeyCode.F1, KeyCode.LeftAlt, KeyCode.LeftBracket, KeyCode.RightBracket, KeyCode.R};

	public enum Axis{PITCH,ROLL,YAW,THROTTLE};
	static InputAxis[] axes = new InputAxis[4];

	void Start () {
		axes [0] = new InputAxis ("Pitch", 0.2, 20, true, 0);
		axes [1] = new InputAxis ("Roll", 0.1, 20, false, 0);
		axes [2] = new InputAxis ("Yaw", 0.2, 20, false, 0);
		axes [3] = new InputAxis ("Throttle", 0.1, 0.5, true, 1);
        print("START "+axes+"\t"+axes[3]);
		if (instance == null) {
			instance = this;
		} else {
			Destroy (this);
		}
	}

	public static bool getButton(Button button){
		bool result = Input.GetButton (joyButtons [(int)button]) || Input.GetKey (keyButtons [(int)button]);
		return result; 
	}

	public static bool getButtonUp(Button button){
		bool result = Input.GetButtonUp (joyButtons [(int)button]) || Input.GetKeyUp (keyButtons [(int)button]);
		return result; 
	}

	public static float getAxis(Axis axis){
		float result = axes [(int)axis].Get ();
		return result;
	}


}

class InputAxis{
	double deadzone;
	double sensitivity;
	string name;
	bool invert;
	double offset;
	double min = -1;
	double max = 1;

	public InputAxis(string name, double deadzone, double sensitivity, bool invert, double offset){
		this.name = name;
		this.deadzone = deadzone;
		this.sensitivity = sensitivity;
		this.invert = invert;
		this.offset = offset;
	}

	public float Get(){
		double result = 0;
		result = Input.GetAxisRaw (name);
		if (name == "Throttle" && Input.GetJoystickNames ().Length == 0) {
			result *= 2;
			if (result < 0) {
				result = 0;
			}
		} else {
			result += offset;
		}
		if (result < deadzone && result > -deadzone) {
			result = 0;	
		}
		result *= sensitivity;
		if (invert) {
			result *= -1;
		}
		result = Mathf.Clamp ((float) result, (float) min, (float) max);
		return (float) result;
	}
}
