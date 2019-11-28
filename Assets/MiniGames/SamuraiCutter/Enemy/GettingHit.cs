using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace SamuraiCutter
{
    public class GettingHit : MonoBehaviour
    {
        public GameObject brokenMesh, replacedMesh;
        MeshRenderer[] brokenPieces;
        public SpawnEnemy spawnEngine;
        public bool bypass, broken;
        public float explosionRadius, explosionPower, timePassed;
        public Vector3 explosionPos;

        private void Awake()
        {
            spawnEngine = GameObject.Find("EnemySpawnManager").GetComponent<SpawnEnemy>();


        }

        private void Update()
        {
            if (bypass)
            {
                Hit(this.transform);
                bypass = false;
                Invoke("DestroyMe", 2f);
            }
        }

        public void Hit(Transform hittingCollider)
        {
            replacedMesh = Instantiate(brokenMesh, transform.position + Vector3.down * 0.2f, transform.rotation);
            
            this.GetComponentInChildren<BoxCollider>().enabled = false;
            this.gameObject.SetActive(false);
            if (!bypass)
            {
                spawnEngine.registerKill();
                explosionPos = hittingCollider.transform.position;
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
                    Invoke("DestroyMe", 2f);
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