using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landingGear : MonoBehaviour
{
    public bool validLanding = true;
    private Rigidbody2D currAsteroid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.name == "Asteroid")
        {
            //Calculates the angle between the objects, and the angle of the ship to make sure they're similiar
            Rigidbody2D ast = other.GetComponent<Rigidbody2D>();
            var resForce = Vector2.zero;
            var dir = ast.position - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y); // get the force direction
            float angleBetween = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;

            float directionPointed = gameObject.GetComponent<Rigidbody2D>().rotation;
            float totalAngle = Math.Abs(directionPointed - angleBetween);

            validLanding = totalAngle <= 15? true : false;
        }
    }
}
