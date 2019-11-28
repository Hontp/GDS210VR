using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public float fireShellTime = 1.5f;

    public GameObject CannonShell;
    public GameObject SideShell;
    public List<Transform> CannonPoints = new List<Transform>();

    private float timePassed = 0;
    private bool isFired;

    private void Update()
    { 
        if (CannonShell == null)
            return;

        if (CannonPoints == null)
            return;


        timePassed += Time.deltaTime;

        if (timePassed >= fireShellTime)
        {
            isFired = false;
        }

        if (!isFired)
        {
            Fire();
        }
    }

    private void Fire()
    {

        for(int i=0; i<3; i++)
        {
            GameObject centreCannon = Instantiate(CannonShell, CannonPoints[i].position, CannonPoints[i].rotation) as GameObject;
            centreCannon.GetComponent<Rigidbody>().velocity = 20.0f * CannonPoints[i].forward;

            GameObject sideCannons = Instantiate(SideShell, CannonPoints[i].position, CannonPoints[i].rotation) as GameObject;
            sideCannons.GetComponent<Rigidbody>().velocity = 15.0f * CannonPoints[i].forward;
        }
     
        isFired = true;
        timePassed = 0;
    }

}
