using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamuraiCutter;

namespace SamuraiCutter
{
public class Samurai : MonoBehaviour
{
    public float health = 1f;
    public AudioSource playerHit;

    // Update is called once per frame
    void Update()
    {
        health = 1f-GameManager._instance.uIManager.impulse;
        if(health < 0.01f)
        {
            GameManager._instance.dead = true;
            Time.timeScale = 0.1f;
            foreach( EnemyAI enemies in FindObjectsOfType<EnemyAI>())
            {
                Destroy(enemies.gameObject,0.5f);
            }
            GameManager._instance.menuSystem.MenuActive();
        }
    }

    void OnTriggerEnter(Collider c)
    {
        //Debug.Log(c.transform.name);
        if(c.transform.CompareTag("enemysword"))
        {
             GameManager._instance.uIManager.SetHurt(0.25f);
                playerHit.Play();
        }
    }
}

}