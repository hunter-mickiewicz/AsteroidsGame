using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cargoMod : MonoBehaviour
{
    double storageIncrease = 20;
    bool applied = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Goes through storage and upgrades each item (this can be done specifically for tiers or items if wanted
        if (GetComponent<StatTracker>() != null)
        {
            Dictionary<string, double[]> storage = GetComponent<StatTracker>().storage;

            if(!applied && storage.Count > 0)
            {
                applied = true;
                foreach (var item in storage)
                {
                    {
                        item.Value[1] += storageIncrease;
                        Debug.Log(item.Key + " " + item.Value[1]);

                    }
                }
            }
            
        }
    }


}
