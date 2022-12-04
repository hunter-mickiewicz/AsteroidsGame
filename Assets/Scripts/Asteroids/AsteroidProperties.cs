using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidProperties : MonoBehaviour
{
    public Dictionary<string, double> elements = new Dictionary<string, double>();
    public double totalWeight = 0;
    public Rigidbody2D asteroid;
    public int weightModifier = 4;

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

        //for testing specific tiers
        //int tier = 99;
        //int tier = 79;
        //int tier = 49;
        //int tier = 9;
        
        double relMass = 100 + gen.Next(1, 10);
        
        //calculates which tier the asteroid is in, then calculates elements
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

        /*foreach (KeyValuePair<string, double> entry in elements)
        {
            Debug.Log(entry);
        }*/

        //Mins and maxes calculated for each type:
        //      NOTE: Maybe I should half these? Not sure...
        //  Tier iv:  44.5250468, 124.351128
        //  Tier iii:  30.9211019, 101.50125
        //  Tier ii:   15.6449, 53.482
        //  Tier i: 8.484, 16.94
        //Debug.Log(totalWeight);


        asteroid = GetComponent<Rigidbody2D>();
        asteroid.mass = (float)totalWeight;

        /*foreach(var element in elements)
        {
            Debug.Log(element.Key + " " + element.Value);
        }*/

    }


    void calculateDarkMatter(double rel, System.Random ran) {
        //takes 1/5 of the possible mass to work with
        double workingMass = rel / 10 / weightModifier;

        //calculates what percentage to work with, between 1/2 -> 1
        double percMass = ran.Next(5, 10);

        workingMass *= percMass / 10;
        elements["DarkMatter"] = workingMass;
        rel -= workingMass;

        totalWeight += workingMass * 3.0;

        calculatePlats(rel, ran);
    }

    void calculatePlats(double rel, System.Random ran) {
        //takes 1/3 of the possible mass to work with
        double workingMass = rel / 3;

        //calculates what percentage to work with, between 1/2 -> 1
        double percMass = ran.Next(5, 10);
        workingMass *= (double)percMass / 10;

        string[] elementArray = {"Ruthenium", "Rhodium", "Palladium", "Osmium", "Iridium", "Platinum"};

        //Need to calculate Ruthenium, Rhodium, Palladium, Osmium, Iridium, Platinum
        foreach (string element in elementArray)
        {
            //base of 1/5, can reach as high as 1/7
            double frac = 5;
            frac += ran.Next(0, 2);

            //takes a random chunk out of working mass, assigns in dictionary
            double elemMass = workingMass / frac / weightModifier;

            frac = ((double)(10 + ran.Next(-2, 1)) / 10);
            elemMass *= frac;

            rel -= elemMass;
            elements[element] = elemMass;

            switch (element)
            {
                case "Ruthenium":
                    totalWeight += elemMass * 1.01;
                    break;
                case "Rhodium":
                    totalWeight += elemMass * 1.03;
                    break;
                case "Palladium":
                    totalWeight += elemMass * 1.06;
                    break;
                case "Osmium":
                    totalWeight += elemMass * 1.90;
                    break;
                case "Iridium":
                    totalWeight += elemMass * 1.92;
                    break;
                case "Platinum":
                    totalWeight += elemMass * 1.95;
                    break;
            }
        }

        calculateIndustrials(rel, ran);
    }

    void calculateIndustrials(double rel, System.Random ran)
    {
        //takes 1/2 of the possible mass to work with
        double workingMass = rel / 2;

        //calculates what percentage to work with, between 1/2 -> 1
        double percMass = ran.Next(5, 10);
        workingMass *= (double)percMass / 10;

        string[] elementArray = { "Iron", "Cobalt", "Nickel" };

        foreach (string element in elementArray)
        {
            //base of 1/2, can reach as high as 1/4
            double frac = 2;
            frac += ran.Next(0, 2);

            //takes a random chunk out of working mass, assigns in dictionary
            double elemMass = workingMass / frac / weightModifier;

            frac = ((double)(10 + ran.Next(-2, 1)) / 10);
            elemMass *= frac;

            rel -= elemMass;
            elements[element] = elemMass;
            
            switch (element)
            {
                case "Iron":
                    totalWeight += elemMass * 0.56;
                    break;
                case "Cobalt":
                    totalWeight += elemMass * 0.56;
                    break;
                case "Nickel":
                    totalWeight += elemMass * 0.55;
                    break;
            }
        }

        calculateVolatiles(rel, ran);
    }

    void calculateVolatiles(double rel, System.Random ran)
    {
        //rel is whatever remains after the others took their chunks out

        string[] elementArray = { "Hydrogen", "Carbon", "Nitrogen", "Oxygen" };
        double workingMass = rel;

        foreach(string element in elementArray)
        {
            //doing 7/24, + or - 1/24 (halfway between 1/3 and 1/4)
            double frac = 7;
            frac += ran.Next(-1, 1);

            double elemMass = workingMass * frac / 24 / weightModifier;

            frac = ((double)(10 + ran.Next(-2, 1)) / 10);
            elemMass *= frac;

            rel -= elemMass;
            elements[element] = elemMass;
            switch (element)
            {
                case "Hydrogen":
                    totalWeight += elemMass * 0.01;
                    break;
                case "Carbon":
                    totalWeight += elemMass * 0.12;
                    break;
                case "Nitrogen":
                    totalWeight += elemMass * 0.14;
                    break;
                case "Oxygen":
                    totalWeight += elemMass * 0.15;
                    break;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
