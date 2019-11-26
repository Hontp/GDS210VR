using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDrop
{
    public class Shell : MonoBehaviour
    {
        public float MaxDamage = 5.5f;
        public float ExplosionForce = 10.0f;
        public float MaxLifeTime = 2.0f;
        public float ExplosionRadius = 2.5f;

        private void Start()
        {
            Destroy(gameObject, MaxLifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius, 0);

            for (int i = 0; i < colliders.Length; i++)
            {
                Rigidbody target = colliders[i].GetComponent<Rigidbody>();

                if (!target)
                    continue;

                target.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius);

                PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();

                if (!playerHealth)
                    continue;

                playerHealth.hit = true;

                Destroy(gameObject);
            }
        }
    }
}