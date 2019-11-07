using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingHit : MonoBehaviour
{
    public GameObject brokenMesh, replacedMesh;
    private void Update()
    {
        
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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);
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
        Destroy(gameObject);
        Destroy(replacedMesh);
    }
}
