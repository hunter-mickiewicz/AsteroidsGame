using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatTracker : StatTracker
{
    // Start is called before the first frame update
    void Start()
    {
        this.miningDistance = 0.6;
        this.entityType = "Player";
        this.initialLimit = 5;
        InstantiateStorage();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
