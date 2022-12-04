using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D ship;
    public float vertInput;
    public float horInput;
    public float turnSpeed = 1.0f;
    public float engineThrust = 0.125f;
    public Vector3 totalVelocity = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //if statement. if input, change the global velocity vector

        vertInput = Input.GetAxis("Vertical");
        horInput = -Input.GetAxis("Horizontal");
        ship.AddTorque(horInput * Mathf.Deg2Rad * turnSpeed);
        //transform.Rotate(new Vector3(0, 0, horInput) * turnSpeed * Time.deltaTime);

        if (vertInput != 0)
        {
            ship.AddForce(transform.up * vertInput * engineThrust);
        }
  
    }
}
