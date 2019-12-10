using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetParts : MonoBehaviour
{
    MeshRenderer[] brokenPieces;
    public Canvas plusOne;
    float timePassed;
    bool shouldDestroySelf;
    float timeToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0f;
        brokenPieces = GetComponentsInChildren<MeshRenderer>();

        plusOne.transform.rotation = Quaternion.LookRotation((transform.position - GameObject.Find("Player").transform.position).normalized,Vector3.up);
    }

    private void Update()
    {
        timePassed += Time.unscaledDeltaTime;

        for (int i = 0; i < brokenPieces.Length; i++)
        {
            brokenPieces[i].material.SetFloat("_alphaClip", -1.5f + timePassed*2f);
        }

        if (shouldDestroySelf && timePassed >= timeToDestroy)
        {
            DestroySelf();

        }
    }


    public void DestroyMe(float t)
    {
        shouldDestroySelf = true;
        timeToDestroy = t;
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }


}
