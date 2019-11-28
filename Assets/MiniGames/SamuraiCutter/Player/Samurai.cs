using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamuraiCutter;

namespace SamuraiCutter
{
public class Samurai : MonoBehaviour
{
    public float health = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health = 1f-GameManager._instance.uIManager.impulse;
        if(health < 0.01f)
        {
            GameManager._instance.dead = true;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        //Debug.Log(c.transform.name);
        if(c.transform.CompareTag("enemysword"))
        {
             GameManager._instance.uIManager.SetHurt(0.25f);
        }
    }
}

}