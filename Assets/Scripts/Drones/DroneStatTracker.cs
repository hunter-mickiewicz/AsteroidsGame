using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneStatTracker : StatTracker
{
    // Start is called before the first frame update
    void Start()
    {
        this.entityType = "Drone";
        this.initialLimit = 0;
        InstantiateStorage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
