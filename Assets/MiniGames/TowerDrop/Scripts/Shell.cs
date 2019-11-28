using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDrop
{
    public class Shell : MonoBehaviour
    {
        public float MaxDamage = 5.5f;
        public float MaxLifeTime = 5.5f;

        private void Start()
        {
            Destroy(gameObject, MaxLifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerHealth>().hit = true;
                Destroy(gameObject);
            }

            Destroy(gameObject, MaxLifeTime);
        }
    }
}