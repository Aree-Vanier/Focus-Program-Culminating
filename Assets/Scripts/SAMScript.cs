using UnityEngine;
using System.Collections;
using System.Collections.Generic;

///Script to control SAM sites
public class SAMScript : MonoBehaviour {

    ///Target for the missiles to hit
    public Part[] targets;
    ///A sphere collider used to visually demonstrate range
    SphereCollider rangeSphere;
    ///Missiles
    public List<HomingMissile> missiles;
    ///Calculated range based on sphere
    public float range;
    ///Cooldown in seconds between missiles
    public float cooldown;
    ///Time of last missile fired
    float lastFire;

    void Start() {
        rangeSphere = GetComponent<SphereCollider>();
        //Get the range from the sphere
        range = rangeSphere.radius*1.5f;
        //Make sure the collider is disabled (No longner useful to us)
        rangeSphere.enabled = false;
    }

    void FixedUpdate() {
        //Wait for cooldown
        if (lastFire + cooldown < Time.time && missiles.Count > 0) {
            foreach (Part p in targets) {
                //If an attached target is in range
                if (p.attached) {
					print(Vector3.Distance(transform.position, p.gameObject.transform.position));
                    if (Vector3.Distance(transform.position, p.gameObject.transform.position) < range) {
                        //Set the target
                        missiles[0].target = p.gameObject.transform;
                        //Fire the missile
                        missiles[0].Fire();
                        //Remove the missile
                        missiles.RemoveAt(0);
                        //Log the time
                        lastFire = Time.time;
                    }
                }
            }
        }
    }
}
