using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boosterMod : MonoBehaviour
{
    public Rigidbody2D ship;
    public float boostThrust = 50f;

    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ship.AddForce(transform.up * boostThrust);
        }   
    }
}
