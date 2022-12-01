using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cargoMod : MonoBehaviour
{
    double storageIncrease = 20;

    // Start is called before the first frame update
    void Start()
    {
        if(CompareTag("Player"))
        {
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
