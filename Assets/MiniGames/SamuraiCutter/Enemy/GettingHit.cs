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
        public AudioSource breakHit;
        public AudioSource clinkHit;
        public Blade blade;
        private EnemyAI ai;

        public SkinnedMeshRenderer mr;
        public Material cracked;

        public float minVelToKill;
        private bool beenHit;

        private void Awake()
        {
            beenHit = false;
            spawnEngine = GameObject.Find("EnemySpawnManager").GetComponent<SpawnEnemy>();
            blade = FindObjectOfType<Blade>();
            ai = GetComponent<EnemyAI>();
            breakHit = GameObject.Find("BreakingSFX").GetComponent<AudioSource>();
            clinkHit = GameObject.Find("ClinkHitSFX").GetComponent<AudioSource>();
            
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
                    kill();
                }
                else
                {
                    if(beenHit)
                    {
                        kill();
                    }
                    else
                    {
                        clink();
                        beenHit = true;
                    }
                }
            }
        }


        void kill()
        {
            replacedMesh = Instantiate(brokenMesh, transform.position + Vector3.down * 0.2f, transform.rotation);
            this.GetComponentInChildren<BoxCollider>().enabled = false;
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            spawnEngine.registerKill();
            breakHit.Play();
            Invoke("DestroyMe", 2f);
            FindObjectOfType<Scoring>().ScoringSystem();
        }

        void clink()
        {
            mr.material = cracked;
            clinkHit.Play();
            ai.jumpBack();
        }

        void DestroyMe()
        {
            print("test 1");
            Destroy(gameObject);
            Destroy(replacedMesh);
        }
    }
}