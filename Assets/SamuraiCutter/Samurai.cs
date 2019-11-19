using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamuraiCutter;

namespace SamuraiCutter
{
public class Samurai : MonoBehaviour
{
    public int health = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {
        Debug.Log(c.transform.name);
        if(c.transform.CompareTag("enemysword"))
        {
            health--;
            GameManager._instance.uIManager.SetHurt(0.5f);
        }
    }
}

}