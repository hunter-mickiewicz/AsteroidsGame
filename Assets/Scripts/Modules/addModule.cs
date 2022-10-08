using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addModule : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Component[] scripts = GetComponents<MonoBehaviour>();
        foreach (var s in scripts)
        {
            Debug.Log(s.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter2D(Collider2D ship)
    {
        Component[] scripts = GetComponents(typeof(addModule));
        foreach (var s in scripts) 
        {
            Debug.Log(s);
        }
        Destroy(gameObject);
        //ship.AddComponent
    }
}
