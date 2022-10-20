using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miner : MonoBehaviour
{
    private GameObject closestAsteroid = null;
    private float closest = 0;
    private GameObject[] asteroids;
    private GameObject playerShip;
    private float miningSpeed = 0.01f;
    
    //This will be pulled directly from player
    //private float capacity;

    // Start is called before the first frame update

    //IDEA FOR CLASS
    //mod added to attached rigidbody allows the attached thing to mine asteroid (within a certain range, start with minimal range)
    //  rudimentary test shows we likely need to be <0.6 distance
    void Start()
    {
        asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        playerShip = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        //Check distance
        //Transfer resources from asteroid to ship
        //vector of all asteroids, find closest and check distance is within the range
        //        = GameObject.Find("Player");

        //Do two things--on key up, release the object so we can test for distance on key down.
           
        //Called once no matter how long held
        if (Input.GetKeyDown(KeyCode.M)) 
        {

            //Establishes closest asteroid
            foreach(GameObject ast in asteroids)
            {
                Rigidbody2D currAst = ast.GetComponent<Rigidbody2D>();
                var dist = (currAst.position - new Vector2(playerShip.transform.position.x, playerShip.transform.position.y)).sqrMagnitude;
                if(closestAsteroid == null)
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

            //transfer (at a specific rate)

        }

        if (Input.GetKey(KeyCode.M)) 
        {
            Dictionary<string, double> elements = closestAsteroid.GetComponent<AsteroidProperties>().elements;
            
            //Transfer elements somehow
            foreach(var element in elements)
            {
                Debug.Log(element);
            }
            
        }

        if (Input.GetKeyUp(KeyCode.M))
        {
            closestAsteroid = null;
            closest = 0;
        }
    }
}
