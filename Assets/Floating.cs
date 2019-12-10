using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public float xProgression;
    public float startingYPos;

    private void Start()
    {
        startingYPos = transform.position.y;
    }

    private void Update()
    {
        Hover();
    }
    void Hover()
    {
        transform.position = new Vector3(transform.position.x, NewY(xProgression), transform.position.z);
        ProgressX();
    }


    float NewY(float xValue)
    {
        float output = Mathf.Sin(xValue * Mathf.PI) + startingYPos;
        return output;
    }


    void ProgressX()
    {
        xProgression += 0.5f * Time.deltaTime;
        if (xProgression > 1f)
        {
            xProgression -= 1f;
        }
    }
}
