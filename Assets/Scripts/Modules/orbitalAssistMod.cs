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

    private GameObject orbiter = null; 
    private Rigidbody2D asteroid = null;
    private bool orbiting = false;
    private string orbitType;
    private float orbitDistance = -1;
    public float g;

    // Start is called before the first frame update

    //Need to fix begin/continue/end conditions. Currently has a decaying orbit
    void Start()
    {

        orbiter = gameObject;
        orbitType = orbiter.GetComponent<StatTracker>().entityType;
        asteroid = getClosestAsteroid().GetComponent<Rigidbody2D>();
        g = asteroid.GetComponent<gravity>().g;

    }

    //Called every 0.02 seconds (can verify in project settings. If this is different, it will break shit)
    void FixedUpdate()
    {
        /*
         * CURRENTLY WORKING
         * Eventual plans:
         *  Implement the adjustments via AddForce instead of hijacking the velocity
         *  Some kind of math to determine which orbit direction would be most efficient (if there is a velocity in one direction already)
         *  End test--allow ship to move as normal, drop orbit distance to -1 again
        */

        //check velocity against calculation, adjust as necessary
        if (orbiting)
        {
            //gets the direction and distance between ship and asteroid
            Vector2 dir = orbiter.GetComponent<Rigidbody2D>().position - asteroid.position;
            float dist2 = dir.magnitude;

            //Instantiates the original orbiting distance
            if(orbitDistance == -1)
            {
                orbitDistance = dist2;
            }


            Vector2 orbitInfo = CalcOrbitalVelocity(dist2, dir);

            //Failsafe against a decaying orbit (boosts the velocity if the distance drops below the original orbiting distance)
            //I think this is due to compounding errors in floating point arithmetic
            if(orbitDistance > dist2)
            {
                orbitInfo.x += orbitInfo.x * (float)1.1;
            }

            float horizontalVelocity = orbitInfo.x * Mathf.Sin(orbitInfo.y);
            float verticalVelocity = orbitInfo.x * Mathf.Cos(orbitInfo.y);
            orbiter.GetComponent<Rigidbody2D>().velocity = new Vector2(-horizontalVelocity, verticalVelocity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(orbitType == "")
        {
            orbitType = orbiter.GetComponent<StatTracker>().entityType;
        }

        //Test forcing the velocity (manually setting it).
        //If that works, try incremental changes using thrusters 
        if (BeginTest())
        {

            //checks if the ship is currently orbiting
            if (!orbiting)
            {
                asteroid = getClosestAsteroid().GetComponent<Rigidbody2D>();


                Vector2 dir = orbiter.GetComponent<Rigidbody2D>().position - asteroid.position;
                float dist2 = dir.magnitude;

                //checks to see if there is actually gravitational attraction between ship and asteroid
                if (dist2 <= orbiter.GetComponent<StatTracker>().astGravitationalDist)
                {
                    orbiting = true;
                }
            }
            else if(ContTest())
            {
                orbiting = false;
            }
        }
    }

    //Calculates the total velocity and angle needed to orbit
    Vector2 CalcOrbitalVelocity(float distance, Vector2 direction)
    {

        float velocity = (float)Math.Sqrt((float)((g * (orbiter.GetComponent<Rigidbody2D>().mass + asteroid.GetComponent<Rigidbody2D>().mass)) / distance));
        float angleBetween = Mathf.Atan2(direction.y, direction.x);

        return (new Vector2(velocity, angleBetween));
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
            var dist = (currAst.position - new Vector2(orbiter.transform.position.x, orbiter.transform.position.y)).sqrMagnitude;
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

    bool BeginTest()
    {
        switch (orbitType)
        {
            case "Player":
                return Input.GetKeyDown(KeyCode.O);
            case "Drone":
                asteroid = getClosestAsteroid().GetComponent<Rigidbody2D>();
                var dist = (asteroid.GetComponent<Rigidbody2D>().position - new Vector2(orbiter.transform.position.x, orbiter.transform.position.y)).sqrMagnitude;
                return ((gameObject.GetComponent<DroneStatTracker>().DroneOn()) && (dist <= orbiter.GetComponent<DroneStatTracker>().orbitDistance));
        }

        return false;
    }

    bool ContTest()
    {
        switch (orbitType)
        {
            case "Player":
                return false;
            case "Drone":
                return gameObject.GetComponent<StatTracker>().storageFull;
        }

        return false;
    }

 /*   bool EndTest()
    {
        switch (orbitType)
        {
            case "Player":
                return Input.GetKeyDown(KeyCode.O);
            case "Drone":
                return testDist();
        }

        return false;
    }*/
}
