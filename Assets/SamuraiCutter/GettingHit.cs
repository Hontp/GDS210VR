using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingHit : MonoBehaviour
{
    public GameObject brokenMesh, replacedMesh;
    public SpawnEnemy spawnEngine;
    public bool bypass;

    private void Awake()
    {
        spawnEngine = GameObject.Find("EnemySpawnManager").GetComponent<SpawnEnemy>();
    }

    private void Update()
    {
        if(bypass)
        {
            bypass = false;
            DestroyMe();
        }
    }
    void OnTriggerEnter(Collider hittingCollider)
    {
        Debug.Log(hittingCollider.transform.tag);
        if(hittingCollider.CompareTag("PlayerSword"))
        {
            Hit();
            Invoke("DestroyMe", 2f);
        }
    }

    void Hit()
    {
            replacedMesh = Instantiate(brokenMesh, transform.position, Quaternion.identity);
            this.GetComponent<BoxCollider>().enabled = false;
            this.GetComponent<MeshRenderer>().enabled = false;
            //Gain Score Code Here
    }

    void DestroyMe()
    {
        spawnEngine.TestSpawn();
        Destroy(gameObject);
    }
}
