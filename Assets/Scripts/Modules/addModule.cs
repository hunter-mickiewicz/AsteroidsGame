using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addModule : MonoBehaviour
{
    //Holds a list of all scripts attached to this game object (aside from this script)
    public List<string> scriptsToAdd = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        //Pulls all attached scripts
        List<Component> scripts = new List<Component>();
        GetComponents(typeof(MonoBehaviour), scripts);

        //Loops through each script, pulls out script name and adds it to List above
        foreach(MonoBehaviour obj in scripts)
        {
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
        Destroy(gameObject);

        //adds each module to the player object
        foreach(string scriptName in scriptsToAdd)
        {
            ship.gameObject.AddComponent(System.Type.GetType(scriptName));
        }
    }
}
