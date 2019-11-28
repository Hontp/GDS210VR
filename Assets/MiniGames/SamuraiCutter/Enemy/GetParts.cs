using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetParts : MonoBehaviour
{
    MeshRenderer[] brokenPieces;
    float timePassed;
    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0f;
        brokenPieces = GetComponentsInChildren<MeshRenderer>();
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        for (int i = 0; i < brokenPieces.Length; i++)
        {
            brokenPieces[i].material.SetFloat("_alphaDelay", -1 + timePassed);
        }
    }
}
