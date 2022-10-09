using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidProperties : MonoBehaviour
{
    public int[] elements;

    // Start is called before the first frame update
    void Start()
    {
        /*Tiers of asteroid:
            i.  40% Hydrogen, Carbon, Nitrogen, Oxygen
            ii. 30% Iron, Cobalt, Nickel
            iii.20% Ruthenium, Rhodium, Palladium, Osmium, Iridium, Platinum
            iv. 10% Dark Matter
        */
        //Instantiate random number generator
        System.Random gen = new System.Random();
        int tier = gen.Next(0,100);// (Start, stop)
        switch (tier)
        {
            //tier iv
            case int n when (n > 90):
                Debug.Log("Tier iv");
                break;

            //tier iii
            case int n when (n > 70):
                Debug.Log("Tier iii");
                break;

            //tier ii
            case int n when (n > 40):
                Debug.Log("Tier ii");
                break;

            //tier i
            default:
                Debug.Log("Tier i");
                break;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
