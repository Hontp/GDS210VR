using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingHit : MonoBehaviour
{
    public GameObject brokenMesh, replacedMesh;
    void OnTriggerEnter(Collider hittingCollider)
    {
        if(hittingCollider.tag == "PlayerSword")
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
        Destroy(gameObject);
        Destroy(replacedMesh);
    }
}
