﻿using System.Collections;
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
        public UnityEngine.AI.NavMeshAgent nma;

        private void Awake()
        {
            spawnEngine = GameObject.Find("EnemySpawnManager").GetComponent<SpawnEnemy>();

        }

        private void Update()
        {

            if (bypass)
            {
                bypass = false;
                Hit();
                Invoke("DestroyMe", 2f);
            }
        }
        void OnTriggerEnter(Collider hittingCollider)
        {
            //Debug.Log(hittingCollider.transform.tag);
            if (hittingCollider.CompareTag("PlayerSword"))
            {
                Hit();
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

        void Hit()
        {
            replacedMesh = Instantiate(brokenMesh, transform.position + Vector3.down * 0.2f, transform.rotation);
            this.GetComponentInChildren<BoxCollider>().enabled = false;
            this.gameObject.SetActive(false);
            //this.GetComponentInChildren<MeshRenderer>().enabled = false;
            spawnEngine.registerKill();
            
            //Gain Score Code Here
        }

        void DestroyMe()
        {
            print("test 1");
            Destroy(gameObject);
            Destroy(replacedMesh);
        }
    }
}