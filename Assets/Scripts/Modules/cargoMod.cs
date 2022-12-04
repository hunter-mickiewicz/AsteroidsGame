using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cargoMod : MonoBehaviour
{
    double storageIncrease = 20;

    // Start is called before the first frame update
    void Start()
    {


        //Goes through storage and upgrades each item (this can be done specifically for tiers or items if wanted
        if(GetComponent<StatTracker>() != null)
        {
            Dictionary<string, double[]> storage = GetComponent<StatTracker>().storage;
            foreach (var item in storage)
            {
                item.Value[1] += storageIncrease;
                //Debug.Log(item.Key + " " + item.Value[1]);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
