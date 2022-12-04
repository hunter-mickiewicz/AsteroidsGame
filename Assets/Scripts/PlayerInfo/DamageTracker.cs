using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTracker : MonoBehaviour
{

    public Sprite[] deathSprites;
    public Sprite deathSprite0;
    public Sprite deathSprite1;
    public Sprite deathSprite2;
    public Sprite deathSprite3;
    public double health;
    // Start is called before the first frame update
    void Start()
    {
        deathSprites = new Sprite[] { deathSprite0, deathSprite1, deathSprite2, deathSprite3 };
        health = GetComponent<StatTracker>().health;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            StartCoroutine(deathAnimation());
        }
        
    }

    IEnumerator deathAnimation()
    {
        foreach(Sprite sp in deathSprites)
        {
            GetComponent<SpriteRenderer>().sprite = sp;
            yield return new WaitForSeconds(1);
        }

        Destroy(gameObject);
        
    }

    public void updateDamage(double dmg)
    {
        health -= dmg;
        //Debug.Log(health);
    }
}
