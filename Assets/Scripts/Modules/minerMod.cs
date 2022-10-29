using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class minerMod : MonoBehaviour
{
    private GameObject closestAsteroid = null;
    private float closest = 0;
    private GameObject[] asteroids;
    private GameObject playerShip;
    private double miningSpeed = 0.015625;
    System.Random gen = new System.Random();
    private bool validMine;

    //determines which elements can be mined
    //private int miningTier = 4;

    //This will be pulled directly from player
    //Likely another dictionary,
    //assumption: keys exist for all element types
    //private float capacity;
    Dictionary<string, double> storage;

    // Start is called before the first frame update


    //FUNCTIONALITY:
    //Hold M to mine. Only works within distance
    //If outside of distance at any point, auto fails and must reattempt.


    //IDEA FOR CLASS
    //mod added to attached rigidbody allows the attached thing to mine asteroid (within a certain range, start with minimal range)
    //  rudimentary test shows we likely need to be <0.6 distance
    void Start()
    {
        //asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        playerShip = GameObject.Find("Player");
        storage = playerShip.GetComponent<PlayerStatTracker>().storage;

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
            closestAsteroid = getClosestAsteroid();
            /*foreach (GameObject ast in asteroids)
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
            }*/

            testDist();
            if (!validMine)
            {
                //Player is starting too far away
                Debug.Log("Initial attempt too far away.");
            }

        }

        if (Input.GetKey(KeyCode.M))
        {
            //Maybe have a preference setting for which elements to mine first, or at all?
            //This might be only with a higher tier of miner (or an upgrade), standard just gives random



            //Need distance setting here...
            Dictionary<string, double> elements = closestAsteroid.GetComponent<AsteroidProperties>().elements;

            int numElem = elements.Count - 1;

            int nextElem = gen.Next(0, numElem);

            KeyValuePair<string, double> element = elements.ElementAt(nextElem);

            if (validMine)
            {
                testDist();
                if (validMine)
                {


                    if (element.Value < 1.0)
                    {
                        storage[element.Key] += element.Value;
                        elements[element.Key] -= element.Value;
                        elements.Remove(element.Key);
                    }
                    else
                    {
                        storage[element.Key] += miningSpeed;
                        elements[element.Key] -= miningSpeed;
                    }
                }
                else
                {
                    //Outside of range, player moved away
                    Debug.Log("Outside of range! Get closer and try again.");
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.M))
        {
            closestAsteroid = null;
            closest = 0;
        }
    }

    void testDist()
    {

        var dist = (closestAsteroid.GetComponent<Rigidbody2D>().position - new Vector2(playerShip.transform.position.x, playerShip.transform.position.y)).sqrMagnitude;

        if (dist < 0.7)
        {
            validMine = true;
        }
        else
        {
            validMine = false;
        }
    }

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
