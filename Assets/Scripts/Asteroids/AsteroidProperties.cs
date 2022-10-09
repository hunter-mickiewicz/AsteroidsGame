using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidProperties : MonoBehaviour
{
    public Dictionary<string, int> elements = new Dictionary<string, int>();

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
        int relMass = 100 + gen.Next(1,10);

        //calculates which tier the asteroid is in, falls through to calculate elements
        //takes relative mass, calculates the existence of minerals, subtracts the relative mass
        switch (tier)
        {
            //tier iv
            case int n when (n > 90):
                calculateDarkMatter(relMass, gen);
                break;

            //tier iii
            case int n when (n > 70):
                calculatePlats(relMass, gen);
                break;

            //tier ii
            case int n when (n > 40):
                calculateIndustrials(relMass, gen);
                break;

            //tier i
            default:
                calculateVolatiles(relMass, gen);
                break;
        }

        foreach (KeyValuePair<string, int> entry in elements)
        {
            Debug.Log(entry);
        }

        //Calculate mass dependent on tier and number of elements
    }

    void calculateDarkMatter(int rel, System.Random ran) {
        //takes 1/5 of the possible mass to work with
        int workingMass = rel / 5;

        //calculates what percentage to work with, between 1/2 -> 1
        double percMass = ran.Next(5, 10);
        workingMass *= (int)(percMass / 10);
        elements["DarkMatter"] = workingMass;
        rel -= workingMass;
        Debug.Log("Tier iv: relMass = " + rel);
        calculatePlats(rel, ran);
    }

    void calculatePlats(int rel, System.Random ran) {
        //takes 1/5 of the possible mass to work with
        int workingMass = rel / 3;

        //calculates what percentage to work with, between 1/2 -> 1
        double percMass = ran.Next(5, 10);
        workingMass *= (int)(percMass / 10);

        string[] elementArray = {"Ruthenium", "Rhodium", "Palladium", "Osmium", "Iridium", "Platinum"};

        //Need to calculate Ruthenium, Rhodium, Palladium, Osmium, Iridium, Platinum
        foreach (string element in elementArray)
        {
            //base of 1/5, can reach as high as 1/7
            int frac = 5;
            frac += ran.Next(0, 2);

            //takes a random chunk out of working mass, assigns in dictionary
            int elemMass = workingMass / frac;
            rel -= elemMass;
            elements[element] = elemMass;
        }

        Debug.Log("Tier iii: relMass = " + rel);
        calculateIndustrials(rel, ran);
    }

    void calculateIndustrials(int rel, System.Random ran)
    {
        //takes 1/5 of the possible mass to work with
        int workingMass = rel / 2;

        //calculates what percentage to work with, between 1/2 -> 1
        double percMass = ran.Next(5, 10);
        workingMass *= (int)(percMass / 10);
        string[] elementArray = { "Iron", "Cobalt", "Nickel" };

        foreach (string element in elementArray)
        {
            //base of 1/2, can reach as high as 1/4
            int frac = 2;
            frac += ran.Next(0, 2);

            //takes a random chunk out of working mass, assigns in dictionary
            int elemMass = workingMass / frac;
            rel -= elemMass;
            elements[element] = elemMass;
        }

        Debug.Log("Tier ii: relMass = " + rel);
        calculateVolatiles(rel, ran);
    }

    void calculateVolatiles(int rel, System.Random ran)
    {
        //rel is whatever remains after the others took their chunks out

        string[] elementArray = { "Hydrogen", "Carbon", "Nitrogen", "Oxygen" };
        foreach(string element in elementArray)
        {
            //doing 7/24, + or - 1/24 (halfway between 1/3 and 1/24
            int frac = 7;
            frac += ran.Next(-1, 1);
            
            int elemMass = rel / (frac / 24);
            rel -= elemMass;
            elements[element] = elemMass;
        }

        Debug.Log("Tier i: relMass = " + rel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
