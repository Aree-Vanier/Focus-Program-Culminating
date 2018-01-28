using UnityEngine;
using System.Collections.Generic;

///Singleton Game Controler runs on levels
public class GameControl : MonoBehaviour {


    public static GameControl instance;

    public float target;
    public Building[] objectives;
    int startObjectiveAmount;
    public PlaneWrapper plane;

    void Start() {
        if(instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
        startObjectiveAmount = objectives.Length;
    }

    void Update() {
        List<Building> temp = new List<Building>();
        foreach(Building b in objectives) {
            //If the objective hasn't been destroyed, add it to temp
            if (!b.destroyed) {
                temp.Add(b);
            }
        }
        //Update objectives
        objectives = temp.ToArray();

    }
}
