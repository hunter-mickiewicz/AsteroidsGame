using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbitalAssistMod : MonoBehaviour
{
    //This will likely need to be much more complicated than thought. https://en.wikipedia.org/wiki/Orbit_equation
    //First thought is the code can take into account current velocity and attempt to make an elliptical orbit based on that.
    //Further velocity changes may change the elliptical nature.
    //find some way to calculate the apses? The velocity/distance at the farthest and closest point of the orbit. Once they match, should be good?
    public float closest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Returns the GameObject asteroid which is the closest to the player.
    public GameObject getClosestAsteroid()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        GameObject playerShip = GameObject.Find("Player");
        GameObject closestAsteroid = null;

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
