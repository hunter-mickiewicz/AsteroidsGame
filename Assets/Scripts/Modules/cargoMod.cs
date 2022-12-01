using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cargoMod : MonoBehaviour
{
    double storageIncrease = 20;

    // Start is called before the first frame update
    void Start()
    {

        //Ensures the mod is attached to the player
        if(CompareTag("Player"))
        {

            //Goes through storage and upgrades each item (this can be done specifically for tiers or items if wanted
            Dictionary<string, double[]> storage = GetComponent<PlayerStatTracker>().storage;
            foreach(var item in storage)
            {
                item.Value[1] += storageIncrease;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
