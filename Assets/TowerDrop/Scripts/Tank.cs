using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public int ShellBatch = 5;
    public float fireShellTime = 5.0f;

    public GameObject CannonShell;
    public GameObject SideShell;
    public List<Transform> CannonPoints = new List<Transform>();

    private int shellCount =0;
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

        if (!isFired && shellCount <= ShellBatch)
        {
            Fire();
        }
    }

    private void Fire()
    {
        int pick = Random.Range(1, 3);

        GameObject centreCannon = Instantiate(CannonShell, CannonPoints[0].position, CannonPoints[0].rotation) as GameObject;
        centreCannon.GetComponent<Rigidbody>().velocity = 20.0f * CannonPoints[0].forward;

        GameObject sideCannons = Instantiate(SideShell, CannonPoints[pick].position, CannonPoints[pick].rotation) as GameObject;
        sideCannons.GetComponent<Rigidbody>().velocity = 15.0f * CannonPoints[pick].forward;
        
        isFired = true;
        timePassed = 0;
        shellCount++;
    }

}
