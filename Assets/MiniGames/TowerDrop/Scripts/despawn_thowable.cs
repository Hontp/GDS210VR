using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class despawn_thowable : MonoBehaviour
{
    public bool explode;
    bool active=false;
    bool triggered=false;
    public GameObject explosion;
    // Start is called before the first frame update
    private void Update()
    {
        if(transform.parent!=null && transform.parent.tag == "hand")
        {
            active = false;
        }
        else
        {
            active = true;
        }
        if(active=true && transform.parent == null)
        {
            triggered = true;
            Destroy(GetComponent<Throwable>(),Time.deltaTime);
            Destroy(GetComponent<Interactable>(),Time.deltaTime);
            Destroy(gameObject, 10);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (explode == true &&triggered==true &&collision.gameObject.tag!="hand")
        {
            GameObject x;
            x= Instantiate(explosion, transform);
            x.transform.parent = null;
            Destroy(gameObject);
        }
        
    }
}
