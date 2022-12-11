using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour
{
    public float g = 6674;
    private List<GameObject> affectedObject = new List<GameObject>();
    private Rigidbody2D currAsteroid;

    // Start is called before the first frame update
    void Start()
    {
        affectedObject.Add(GameObject.Find("Player"));
        GameObject[] drones = GameObject.FindGameObjectsWithTag("Drone");
        foreach(GameObject drone in drones)
        {
            affectedObject.Add(drone);
        }
        //Need to go through all object with "Drone" tags
        //(and possibly others as I add other things to the game)

        currAsteroid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject attracted in affectedObject)
        {
            var resForce = Vector2.zero;
            var dir = currAsteroid.position - new Vector2(attracted.transform.position.x, attracted.transform.position.y); // get the force direction
            var dist1 = dir.magnitude;

            float dist2 = dir.sqrMagnitude; // get the squared distance

            //Only have gravity affect it if it's close enough (need an in-universe explanation here)
            if (dist1 < attracted.GetComponent<StatTracker>().astGravitationalDist && dist2 != 0)
            {
                // calculate the force intensity using Newton's law
                float gForce = (float)(g * attracted.GetComponent<Rigidbody2D>().mass * currAsteroid.GetComponent<Rigidbody2D>().mass / dist2);

                resForce += gForce * dir.normalized; // accumulate in the resulting force variable

                attracted.GetComponent<Rigidbody2D>().AddForce(resForce);
            }

        }
    }

    void OnTriggerStay2D(Collider2D ship)
    {
        //Placeholder -- actual damage should be a function of gravity (higher g force, more damage)
        //Also, if there is a landing module on the ship (and it's oriented correctly) there is no damage taken
        if (ship.GetComponent<landingGear>() == null || ship.GetComponent<landingGear>().validLanding != true)
        {
            ship.GetComponent<DamageTracker>().updateDamage(1);
        }
    }
}
