using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace SamuraiCutter
{
    public class GettingHit : MonoBehaviour
    {
        public GameObject brokenMesh, replacedMesh;
        public SpawnEnemy spawnEngine;
        public bool bypass;
        public float explosionRadius, explosionPower;
        public Vector3 explosionPos;

        public Blade blade;
        private EnemyAI ai;

        public float minVelToKill;

        private void Awake()
        {
            spawnEngine = GameObject.Find("EnemySpawnManager").GetComponent<SpawnEnemy>();
            blade = FindObjectOfType<Blade>();
            ai = GetComponent<EnemyAI>();
        }

        private void Update()
        {
            if (bypass)
            {
                //Hit(this.transform);
                bypass = false;
                Invoke("DestroyMe", 2f);
            }
        }

        public void Hit()
        {
            if (!bypass)
            {
                explosionPos = this.transform.position;
                Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
                foreach (Collider hit in colliders)
                {
                    if (hit.CompareTag("EnemyChunk"))
                    {
                        Rigidbody rb = hit.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.AddExplosionForce(explosionPower, explosionPos, explosionRadius, 3.0f);
                        }
                    }
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("Hitting");
            if(other.CompareTag("PlayerSword"))
            {
                if(blade.averageVel >= minVelToKill)
                {
                    replacedMesh = Instantiate(brokenMesh, transform.position + Vector3.down * 0.2f, transform.rotation);
                    this.GetComponentInChildren<BoxCollider>().enabled = false;
                    this.gameObject.SetActive(false);
                    spawnEngine.registerKill();
                    Invoke("DestroyMe", 2f);
                }
                else
                {
                    ai.jumpBack();
                }
            }
        }

        void DestroyMe()
        {
            print("test 1");
            Destroy(gameObject);
            Destroy(replacedMesh);
        }
    }
}