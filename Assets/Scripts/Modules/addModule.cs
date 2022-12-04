using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addModule : MonoBehaviour
{
    //Holds a list of all scripts attached to this game object (aside from this script)
    public List<string> scriptsToAdd = new List<string>();
    private bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        //Pulls all attached scripts
        List<Component> scripts = new List<Component>();
        GetComponents(typeof(MonoBehaviour), scripts);
        Debug.Log(scripts.Count);

        //Loops through each script, pulls out script name and adds it to List above
        foreach(MonoBehaviour obj in scripts)
        {
            Debug.Log(obj.ToString());
            if (!obj.ToString().Contains("addModule"))
            {
                string tempName = obj.ToString();
                int paren = tempName.IndexOf("(") + 1;
                int len = tempName.IndexOf(")") - paren;
                tempName = tempName.Substring(paren, len);
                scriptsToAdd.Add(tempName);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D ship)
    {
        //Need to check if it's colliding, otherwise some colliders will send a collision multiple times
        if (isColliding) return;
        isColliding = true;
        Destroy(gameObject);
        Debug.Log(scriptsToAdd.Count);

        //adds each module to the player object
        foreach(string scriptName in scriptsToAdd)
        {
            Debug.Log(scriptName);
            ship.gameObject.AddComponent(System.Type.GetType(scriptName));
        }
    }
}
