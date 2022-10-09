using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addModule : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter2D(Collider2D ship)
    {
        //Runtime determination of script name was a bust... have to manually enter
        //script name for each thing.
        Destroy(gameObject);
        ship.gameObject.AddComponent<boosterMod>();
    }
}
