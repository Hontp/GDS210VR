using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class MagazineScript : MonoBehaviour
{
    public Hand hand;
    public GameObject x;
    public static bool isLoaded = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MagSnapPoint")
        {
            for (int i = 0; hand.AttachedObjects.Count > i; i++)
            {
                if (hand.AttachedObjects[i].attachedObject.name == "Mag")
                {
                    x = hand.AttachedObjects[i].attachedObject.gameObject;

                    hand.DetachObject(hand.AttachedObjects[i].attachedObject, false);
                    x.transform.parent = hand.gameObject.transform;
                    x.GetComponent<Throwable>().enabled = false;
                    Destroy(x.GetComponent<Rigidbody>());
                    isLoaded = true;
                }
            }
        }
    }
}
