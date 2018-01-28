using UnityEngine;
using System.Collections.Generic;

///Singleton Game Controler runs on levels
public class GameControl : MonoBehaviour {


    public static GameControl instance;

    public float target;
    public Building[] objectives;
    int startObjectiveAmount;
    public PlaneWrapper plane;
    public float completion;

    void Start() {
        //Singleton Pattern
        if(instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
        startObjectiveAmount = objectives.Length;
    }

    void Update() {
        ///Temporary list to hold non-destroyed buildings
        List<Building> temp = new List<Building>();
        foreach(Building b in objectives) {
            //If the objective hasn't been destroyed, add it to temp
            if (!b.destroyed) {
                temp.Add(b);
            }
        }
        //Update objectives
        objectives = temp.ToArray();
        completion = objectives.Length / startObjectiveAmount;

    }

    ///Calls planeWrapper functions to control the plane
    void ControlPlane() {
        //Toggle gear
        if (InputManager.getButtonUp(InputManager.Button.GEAR)) {
            plane.ToggleGear();
        }
        //Toggle Canopy
        if (InputManager.getButtonUp(InputManager.Button.CANOPY)) {
            plane.ToggleCanopy();
        }
        //Deprecated because they are useless
  //      //Toggle Tail Hook
  //      if (Input.GetKeyUp(KeyCode.H)) {
  //          plane.ToggleHook();
  //      }
  //      //Eject
		//if (InputManager.getButton(InputManager.Button.EJECT)) {
		//	plane.Eject();
		//}

        //Plane physics functions
        plane.Propel(InputManager.getAxis(InputManager.Axis.THROTTLE));
		plane.Pitch(InputManager.getAxis(InputManager.Axis.PITCH));
		plane.Roll(InputManager.getAxis(InputManager.Axis.ROLL));
		plane.Yaw(InputManager.getAxis(InputManager.Axis.YAW));
	}
    }
}
