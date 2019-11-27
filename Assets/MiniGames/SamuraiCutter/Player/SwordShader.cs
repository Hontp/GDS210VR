using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordShader : MonoBehaviour
{
    public LineRenderer lineRenderer;
    // Start is called before the first frame update
    public Vector2 bladeOffset;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
