using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDrone : MonoBehaviour
{
    //Need to drag the drone from prefab to script in editor
    public GameObject droneToSpawn;
    private GameObject spawner;
    private float launchSpeed = 5;
    private string spawnerType = "";


    // Start is called before the first frame update
    void Start()
    {
        spawner = gameObject;
        spawnerType = spawner.GetComponent<StatTracker>().entityType;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnerType == "")
        {
            spawnerType = spawner.GetComponent<StatTracker>().entityType;
        }

        if (SpawnTest())
        {
            GameObject drone = Instantiate(droneToSpawn, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
            drone.transform.SetParent(transform);
            drone.transform.localPosition = new Vector3(0, (float)0.4, 0);
            //FIXME: launches vertically up, needs to be relative
            drone.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector3(0,1,0) * launchSpeed);

        }
        
    }

    bool SpawnTest()
    {
        switch (spawnerType)
        {
            case "Player":
                return Input.GetKeyDown(KeyCode.Q);
        }

        return false;
    }
}
