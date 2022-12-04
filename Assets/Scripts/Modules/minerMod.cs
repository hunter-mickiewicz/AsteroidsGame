using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class minerMod : MonoBehaviour
{
    private GameObject closestAsteroid = null;
    private float closest = 0;
    private GameObject[] asteroids;
    private GameObject miner;
    private double miningSpeed = 0.015625;
    System.Random gen = new System.Random();
    private bool validMine;
    private string minerType;

    //determines which elements can be mined
    //private int miningTier = 4;


    //assumption: keys exist for all element types
    Dictionary<string, double[]> storage;

    // Start is called before the first frame update


    //FUNCTIONALITY:
    //Hold M to mine. Only works within distance
    //If outside of distance at any point, auto fails and must reattempt.


    //IDEA FOR CLASS
    //mod added to attached rigidbody allows the attached thing to mine asteroid (within a certain range, start with minimal range)
    //  rudimentary test shows we likely need to be <0.6 distance
    void Start()
    {
        asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        minerType = GetComponent<StatTracker>().entityType;
        miner = gameObject;
        storage = gameObject.GetComponent<StatTracker>().storage;

    }

    // Update is called once per frame
    /* Mostly good. Changes to Drone:
     * Should do something different than just print that it's full of storage. 
     * Likely shouldn't print errors, assuming it's automated.
    */
    void Update()
    {
        //Check distance
        //Transfer resources from asteroid to ship
        //vector of all asteroids, find closest and check distance is within the range
        //        = GameObject.Find("Player");

        //Do two things--on key up, release the object so we can test for distance on key down.

        //Called once no matter how long held
        if (BeginTest())
        {

            //Establishes closest asteroid
            closestAsteroid = getClosestAsteroid();

            testDist();
            if (!validMine)
            {
                //Player is starting too far away
                Debug.Log("Initial attempt too far away.");
            }

        }

        if (ContTest())
        {
            //Maybe have a preference setting for which elements to mine first, or at all (eventual)?
            //This might be only with a higher tier of miner (or an upgrade), standard just gives random



            //Need distance setting here...
            Dictionary<string, double> elements = closestAsteroid.GetComponent<AsteroidProperties>().elements;

            //Tests if we're initially in range, then again if we pull out of range during the mining
            if (validMine)
            {
                if (elements.Count == 0)
                {
                    Debug.Log("Asteroid is fully mined!");
                    validMine = false;
                    return;
                }

                int numElem = elements.Count;

                int nextElem;// = gen.Next(0, numElem);
                KeyValuePair<string, double> element;// = elements.ElementAt(nextElem);
                double remainder;

                //These next two blocks ensure we don't enter an infinite loop.
                //If I add in storage tiers (or mining tiers even) I will need to adjust accordingly.
                int cargoSpace = 0;
                foreach(var item in storage)
                {
                    if(miner.GetComponent<StatTracker>().GetRemainingCapacity(item.Key) != 0 && elements.ContainsKey(item.Key))
                    {
                        cargoSpace++;
                    }
                }

                if (cargoSpace == 0)
                {
                    Debug.Log("All storage capacity full!");
                    validMine = false;
                    return;
                }

                do
                {
                    nextElem = gen.Next(0, numElem);
                    element = elements.ElementAt(nextElem);

                    //pulls the remaining capacity for the current element from player
                    remainder = miner.GetComponent<StatTracker>().GetRemainingCapacity(element.Key);

                }
                while (remainder == 0 && cargoSpace != 0);

                testDist();
                if (validMine)
                {
                    //Debug.Log(element.Key);

                    //(this will fall through to maximize code and improve efficiency)
                    if (remainder > miningSpeed)
                    { 
                        //Remainder is assigned the lower of the two values (element.Value if mining speed would strip what's left in the asteroid
                        remainder = miningSpeed > element.Value ? element.Value : miningSpeed;
                    }

                    //Not enough remaining capacity, 
                    else
                    {
                        remainder = remainder > element.Value ? element.Value : remainder;
                    }

                    if(remainder == 0.0)
                    {
                        Debug.Log(element.Key + " storage is full!");
                        gameObject.GetComponent<StatTracker>().storageFull = true;
                        //Want logic here to flag if the storage is full and prevent going down the loop again
                    }
                    else
                    {
                        storage[element.Key][0] += remainder;
                        elements[element.Key] -= remainder;
                        if (elements[element.Key] == 0.0)
                        {
                            elements.Remove(element.Key);
                        }
                    }
                }
                else
                {
                    //Outside of range, player moved away
                    Debug.Log("Outside of range! Get closer and try again.");
                }
            }
        }

        if (EndTest())
        {
            closestAsteroid = null;
            closest = 0; 
            
            //Uncomment lines to test value of storage items
            /*foreach(var item in storage)
            {
                Debug.Log(item.Key + item.Value[0]);
            }*/
        }
    }

    bool BeginTest()
    {
        switch (minerType)
        {
            case "Player":
                return Input.GetKeyDown(KeyCode.M);
            case "Drone":
                return gameObject.GetComponent<DroneStatTracker>().DroneOn();
        }

        return false;
    }

    bool ContTest()
    {
        switch (minerType)
        {
            case "Player":
                return Input.GetKey(KeyCode.M);
            case "Drone":
                return testDist();
        }

        return false;
    }

    bool EndTest()
    {
        switch (minerType)
        {
            case "Player":
                return Input.GetKeyUp(KeyCode.M);
            case "Drone":
                return !testDist();
        }

        return false;
    }

    bool testDist()
    {

        var dist = (closestAsteroid.GetComponent<Rigidbody2D>().position - new Vector2(miner.transform.position.x, miner.transform.position.y)).sqrMagnitude;

        if (dist < miner.GetComponent<StatTracker>().miningDistance)
        {
            validMine = true;
            Debug.Log(dist);
        }
        else
        {
            validMine = false;
        }

        return validMine;
    }

    public GameObject getClosestAsteroid()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        GameObject miner = GameObject.Find("Player");
        GameObject closestAsteroid = null;

        foreach (GameObject ast in asteroids)
        {
            Rigidbody2D currAst = ast.GetComponent<Rigidbody2D>();
            var dist = (currAst.position - new Vector2(miner.transform.position.x, miner.transform.position.y)).sqrMagnitude;
            if (closestAsteroid == null || dist < closest)
            {
                closestAsteroid = ast;
                closest = dist;
                Debug.Log(dist);
            }
        }

        return closestAsteroid;
    }
}
