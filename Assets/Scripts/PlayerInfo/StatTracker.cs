using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTracker : MonoBehaviour
{

    public string entityType;
    public double health = 100;
    //Storage for elements. first item in array is the amount in storage, second is the limit
    public Dictionary<string, double[]> storage = new Dictionary<string, double[]>();

    public int astGravitationalDist = 10;
    public double initialLimit = 0;
    public double miningDistance;

    // Start is called before the first frame update
    void Start()
    {
 

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Returns the remaining capacity for a certain element
    public double GetRemainingCapacity(string item) 
    {
        return storage[item][1] - storage[item][0];
    }

    protected void InstantiateStorage()
    {
        string[] elements = { "DarkMatter", "Ruthenium", "Rhodium", "Palladium", "Osmium", "Iridium", "Platinum", "Iron", "Cobalt", "Nickel",
            "Hydrogen", "Carbon", "Nitrogen", "Oxygen" };

        foreach (string element in elements)
        {
            storage[element] = new double[] { 0.0, initialLimit };
        }

    }

}
