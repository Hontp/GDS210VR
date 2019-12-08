using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eplosion_script : MonoBehaviour
{
    float size_by_time=0;
    public float groth;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        size_by_time += Time.deltaTime;
        transform.localScale = new Vector3(1,1,1)* size_by_time * groth;
    }
}
