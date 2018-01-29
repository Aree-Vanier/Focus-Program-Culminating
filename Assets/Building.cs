using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Class to handle destroyable buildings
public class Building : MonoBehaviour {

    ///Destroyed flag, true when building is destroyed
    public bool destroyed;
    Rigidbody body;
    ///Health of the building, destoyed when health = 0
    public int health = 1000;


	void Start () {
        body = GetComponent<Rigidbody>();
	}
	
    void Update() {
        if(health < 0) {
            Destroy();
        }
    }

    ///Damage the building
    ///Damage: Amount of damage done to the building
    public void Shoot(int damage) {
        health -= damage;
    }

    ///Destroy the building
    public void Destroy() {
        destroyed = true;
        //If there is an explosion, detonate it
        Explosion e = GetComponentInChildren<Explosion>();
        if (e != null) {
			print ("BOOM");
            e.Detonate();
        }
        //Diable the collider so that the building falls through the ground
        GetComponent<Collider>().enabled = false;
		gameObject.AddComponent<Rigidbody> ();
        //Destroy the gameobject in 5 seconds
		Destroy(this.transform.parent.gameObject, 5);
    }
}
