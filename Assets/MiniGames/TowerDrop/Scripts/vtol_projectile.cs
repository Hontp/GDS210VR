using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vtol_projectile : MonoBehaviour
{
    public float projectile_speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime*projectile_speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag!="vtol")
            Destroy(gameObject);
    }
}
