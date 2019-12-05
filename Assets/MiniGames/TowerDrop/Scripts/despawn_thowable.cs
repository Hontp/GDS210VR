using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class despawn_thowable : MonoBehaviour
{
    public bool explode;
    public GameObject explosion;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (explode == true)
        {
            GameObject x;
            x= Instantiate(explosion, transform);
            x.transform.parent = null;
        }
        Destroy(gameObject, 10);
    }
}
