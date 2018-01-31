using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Script to handle explosion particles and damage
public class Explosion : MonoBehaviour {

    /// Effective diameter of particles and damage, set to 1/5th of collider size
    public int diameter;
    SphereCollider collider;
    ParticleSystem particles;
    /// Detonated flag
    bool detonated = false;
    /// Counter for disabling
    int counter = 0;
    ///Explosion sound
    AudioClip sound;

    void Start () {
	    //Set collider size
	    collider = GetComponent<SphereCollider> ();
	    collider.radius = diameter * 10;
	    //Set explosion size
	    particles = GetComponent<ParticleSystem> ();
	    ParticleSystem.MainModule explosionMain = particles.main;
	    explosionMain.startSize = new ParticleSystem.MinMaxCurve (diameter / 2, diameter);
        //Import explsion sound
        sound = Resources.Load("Audio/Bomb") as AudioClip;
        print(sound);
    }


    /// Start detonation
    public void Detonate() {
        if (!detonated) { 
            particles.Play();
            collider.enabled = true;
            detonated = true;
            //Add the explosion sound
            gameObject.AddComponent<AudioSource>();
            GetComponent<AudioSource>().clip = sound;
            //Play the sound
            GetComponent<AudioSource>().Play();
            Destroy(this, 5);
        }
    }

    void OnTriggerEnter(Collider other){
	    //If its a plane part, detach it
	    if (other.GetComponent<Part> () != null) {
		    print ("Plane");
		    other.GetComponent<Part> ().Detach ();
		    return;
        }
        //If its a building, destroy it
        if (other.GetComponent<Building>() != null) {
            print("Building");
            other.GetComponent<Building>().Destroy();
            return;
		}
		//If its an explosion, detonate it
		if (other.GetComponentInChildren<Explosion>() != null) {
			print("Building");
			other.GetComponent<Explosion>().Invoke("Detonate", 1.5f);
			return;
		}

        //If its not incinvible and it's not a plane part, disable it
        if (other.tag != "Invincible")
		    other.gameObject.SetActive (false);
    }
}
