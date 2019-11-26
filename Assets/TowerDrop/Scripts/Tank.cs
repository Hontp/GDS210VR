using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public Rigidbody CannonShell;
    public List<Transform> CannonPoints = new List<Transform>();

    private bool isFired;

    private void Update()
    {
        if (CannonShell == null)
            return;

        if (CannonPoints == null)
            return;

        Rigidbody LeftshellInstance = Instantiate(CannonShell, CannonPoints[0].position, CannonPoints[0].rotation) as Rigidbody;
        LeftshellInstance.velocity = 20.0f * CannonPoints[0].forward;
    }


}
