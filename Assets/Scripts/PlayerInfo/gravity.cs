using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour
{
    public float g = 1f;
    private GameObject playerShip;
    private Rigidbody2D currAsteroid;

    // Start is called before the first frame update
    void Start()
    {
        playerShip = GameObject.Find("Player");

        currAsteroid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {    
        //If the ship and asteroid collide, it glitches hard. How can I fix this?

        var resForce = Vector2.zero;
        var dir = currAsteroid.position - new Vector2(playerShip.transform.position.x, playerShip.transform.position.y); // get the force direction
        
        float dist2 = dir.sqrMagnitude; // get the squared distance

        //Only have gravity affect it if it's close enough (need an in-universe explanation here)
        if (dist2 < playerShip.GetComponent<PlayerStatTracker>().astGravitationalDist && dist2 != 0)
        {
            // calculate the force intensity using Newton's law
            float gForce = (float)(playerShip.GetComponent<Rigidbody2D>().mass * currAsteroid.GetComponent<Rigidbody2D>().mass / dist2);

            resForce += gForce * dir.normalized; // accumulate in the resulting force variable

            playerShip.GetComponent<Rigidbody2D>().AddForce(resForce);
        }
    }

    void OnTriggerStay2D(Collider2D ship)
    {
        //Placeholder -- actual damage should be a function of gravity (higher g force, more damage)
        //Also, if there is a landing module on the ship (and it's oriented correctly) there is no damage taken
        ship.GetComponent<DamageTracker>().updateDamage(1);
    }
}
