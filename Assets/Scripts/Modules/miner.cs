using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miner : MonoBehaviour
{
    private Rigidbody2D closestAsteroid;
    // Start is called before the first frame update

    //IDEA FOR CLASS
    //mod added to attached rigidbody allows the attached thing to mine asteroid (within a certain range, start with minimal range)
    //  rudimentary test shows we likely need to be <0.6 distance
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check distance
        //Transfer resources from asteroid to ship
        //vector of all asteroids, find closest and check distance is within the range
//        = GameObject.Find("Player");

    }
}
