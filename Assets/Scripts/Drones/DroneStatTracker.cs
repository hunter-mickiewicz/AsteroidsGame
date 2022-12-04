using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneStatTracker : StatTracker
{
    // Start is called before the first frame update
    void Start()
    {
        this.miningDistance = 1.0;
        this.entityType = "Drone";
        this.initialLimit = 0;
        InstantiateStorage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Function to determine if the drone has been turned on
    //This will probably change depending on the type of drone (proximity, time, trigger, etc)
    public bool DroneOn()
    {
        return true;
    }
}
