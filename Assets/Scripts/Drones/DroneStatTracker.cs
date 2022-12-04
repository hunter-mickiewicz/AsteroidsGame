using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneStatTracker : StatTracker
{
    public double orbitDistance;
    // Start is called before the first frame update
    void Start()
    {
        this.miningDistance = 1.5;
        this.entityType = "Drone";
        this.initialLimit = 0;
        this.orbitDistance = this.miningDistance - 0.1;
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
