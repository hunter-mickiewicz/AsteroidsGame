using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidProperties : MonoBehaviour
{
    public Dictionary<string, double> elements = new Dictionary<string, double>();

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
        //int tier = gen.Next(0,100);// (Start, stop)
        int tier = 99;
        double relMass = 100 + gen.Next(1,10);

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

        //Uncomment to print out the values generated
        /*
        foreach (KeyValuePair<string, double> entry in elements)
        {
            Debug.Log(entry);
        }
        */

        //Todo: Calculate mass dependent on tier and number of elements
    }

    void calculateDarkMatter(double rel, System.Random ran) {
        //takes 1/5 of the possible mass to work with
        double workingMass = rel / 10;

        //calculates what percentage to work with, between 1/2 -> 1
        double percMass = ran.Next(5, 10);
        workingMass *= percMass / 10;
        elements["DarkMatter"] = workingMass;
        rel -= workingMass;

        calculatePlats(rel, ran);
    }

    void calculatePlats(double rel, System.Random ran) {
        //takes 1/5 of the possible mass to work with
        double workingMass = rel / 3;

        //calculates what percentage to work with, between 1/2 -> 1
        double percMass = ran.Next(5, 10);
        workingMass *= percMass / 10;

        string[] elementArray = {"Ruthenium", "Rhodium", "Palladium", "Osmium", "Iridium", "Platinum"};

        //Need to calculate Ruthenium, Rhodium, Palladium, Osmium, Iridium, Platinum
        foreach (string element in elementArray)
        {
            //base of 1/5, can reach as high as 1/7
            double frac = 5;
            frac += ran.Next(0, 2);

            //takes a random chunk out of working mass, assigns in dictionary
            double elemMass = workingMass / frac;
            rel -= elemMass;
            elements[element] = elemMass;
        }

        calculateIndustrials(rel, ran);
    }

    void calculateIndustrials(double rel, System.Random ran)
    {
        //takes 1/5 of the possible mass to work with
        double workingMass = rel / 2;

        //calculates what percentage to work with, between 1/2 -> 1
        double percMass = ran.Next(5, 10);
        workingMass *= percMass / 10;
        string[] elementArray = { "Iron", "Cobalt", "Nickel" };

        foreach (string element in elementArray)
        {
            //base of 1/2, can reach as high as 1/4
            double frac = 2;
            frac += ran.Next(0, 2);

            //takes a random chunk out of working mass, assigns in dictionary
            double elemMass = workingMass / frac;
            rel -= elemMass;
            elements[element] = elemMass;
        }

        calculateVolatiles(rel, ran);
    }

    void calculateVolatiles(double rel, System.Random ran)
    {
        //rel is whatever remains after the others took their chunks out

        string[] elementArray = { "Hydrogen", "Carbon", "Nitrogen", "Oxygen" };
        foreach(string element in elementArray)
        {
            //doing 7/24, + or - 1/24 (halfway between 1/3 and 1/24
            double frac = 7;
            frac += ran.Next(-1, 1);

            double elemMass = rel * frac / 24;
            rel -= elemMass;
            elements[element] = elemMass;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
