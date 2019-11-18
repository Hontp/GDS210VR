using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Enemy : MonoBehaviour
{

    public void slice(Vector3 planePoint, Vector3 planeUpVector)
    {
        EzySlice.Plane slicePlane = new EzySlice.Plane(planePoint,planeUpVector);
        // TOOD: change to a proper material
        GetComponent<GameObject>().Slice(slicePlane,null);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
