using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbitalAssistMod : MonoBehaviour
{
    //This will likely need to be much more complicated than thought. https://en.wikipedia.org/wiki/Orbit_equation
    //First thought is the code can take into account current velocity and attempt to make an elliptical orbit based on that.
    //Further velocity changes may change the elliptical nature.
    //find some way to calculate the apses? The velocity/distance at the farthest and closest point of the orbit. Once they match, should be good?

    private GameObject playerShip = null; 
    private Rigidbody2D asteroid = null;
    private bool orbiting = false;

    // Start is called before the first frame update
    void Start()
    {
        playerShip = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Test forcing the velocity (manually setting it).
        //If that works, try incremental changes using thrusters 
        if (Input.GetKeyDown(KeyCode.O))
        {

            //checks if the ship is currently orbiting
            if (!orbiting) {
                asteroid = getClosestAsteroid().GetComponent<Rigidbody2D>();

                //gets the direction and distance between ship and asteroid
                var dir = asteroid.position - new Vector2(playerShip.transform.position.x, playerShip.transform.position.y);
                float dist2 = dir.magnitude;

                //checks to see if there is actually gravitational attraction between ship and asteroid
                if (dist2 <= playerShip.GetComponent<PlayerStatTracker>().astGravitationalDist)
                {
                    //gets the magnitude of gravitational force

                    //For eliptical orbits
                    //float velocity = (float)Math.Sqrt(playerShip.GetComponent<Rigidbody2D>().mass + asteroid.GetComponent<Rigidbody2D>().mass * (2 / dist2) - (1 / dist2));
                    float velocity = (float)(playerShip.GetComponent<Rigidbody2D>().mass * asteroid.GetComponent<Rigidbody2D>().mass * ((2 / dist2) - (1 / dist2)));
                    Debug.Log(asteroid.GetComponent<Rigidbody2D>().mass);
                    //for circular orbits
                    //float velocity = (float)Math.Sqrt((float)((playerShip.GetComponent<Rigidbody2D>().mass + asteroid.GetComponent<Rigidbody2D>().mass) / dist2));
                    //float velocity = (float)(Math.Sqrt(((playerShip.GetComponent<Rigidbody2D>().mass * asteroid.GetComponent<Rigidbody2D>().mass) / dist2)));
                    //Debug.Log(velocity);

                    //Need the sin/cos of the ANGLE, not the velocity
                    double angle = 0 * Math.PI / 180;
                    Vector2 currVel = playerShip.GetComponent<Rigidbody2D>().velocity;
                    //playerShip.GetComponent<Rigidbody2D>().velocity = new Vector2((float)(velocity * Math.Cos(angle) + currVel[0]), (float)(velocity * Math.Sin(angle) + currVel[1]));
                    Vector2 newVel = new Vector2((float)(velocity), (float)(currVel[1]));
                    //playerShip.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    playerShip.GetComponent<Rigidbody2D>().velocity += newVel;
                    // playerShip.GetComponent<Rigidbody2D>().AddForce(newVel.normalized);

                }
            }
            else
            {

            }
        }

    }

    //Returns the GameObject asteroid which is the closest to the player.
    public GameObject getClosestAsteroid()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        GameObject closestAsteroid = null;
        float closest = 0;

        foreach (GameObject ast in asteroids)
        {
            Rigidbody2D currAst = ast.GetComponent<Rigidbody2D>();
            var dist = (currAst.position - new Vector2(playerShip.transform.position.x, playerShip.transform.position.y)).sqrMagnitude;
            if (closestAsteroid == null)
            {
                closestAsteroid = ast;
                closest = dist;
            }
            else if (dist < closest)
            {
                closestAsteroid = ast;
                closest = dist;
            }
        }

        return closestAsteroid;
    }
}
