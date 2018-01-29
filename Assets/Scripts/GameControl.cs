using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

///Singleton Game Controler runs on levels
public class GameControl : MonoBehaviour {


    public static GameControl instance;

    public double target;
    public Building[] objectives;
    public double startObjectiveAmount;
    public PlaneWrapper plane;
    public double completion;

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
        if(startObjectiveAmount != 0)
			completion = Mathf.Abs((float) (1 - objectives.Length / startObjectiveAmount));

		if (completion > target) {
			PlayerPrefs.SetString("GOTitle", "Mission Success");
			PlayerPrefs.SetString("GOText", "You destroyed the enemy base");
			Invoke ("Win", 5);
		}
    }

	/// Called to change scene after winning, used so that there can be some delay between winning and game over
	void Win(){
		//Change scene
		SceneManager.LoadScene (1);
	}

    void FixedUpdate() {
        ControlPlane();    
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
