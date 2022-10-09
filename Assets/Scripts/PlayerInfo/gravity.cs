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
        
        var dist2 = dir.sqrMagnitude; // get the squared distance

        //Only have gravity affect it if it's close enough (need an in-universe explanation here)
        if (dist2 < 20)
        {
            // calculate the force intensity using Newton's law
            var gForce = g * playerShip.GetComponent<Rigidbody2D>().mass * currAsteroid.GetComponent<Rigidbody2D>().mass / dist2;
            resForce += gForce * dir.normalized; // accumulate in the resulting force variable

            playerShip.GetComponent<Rigidbody2D>().AddForce(resForce);
            currAsteroid.AddForce(-resForce);
        }
    }
}
