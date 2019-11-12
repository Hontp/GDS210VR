using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class SwordHitting : MonoBehaviour
{
    public SteamVR_Action_Vibration vibration;
    public SteamVR_Input_Sources inputSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            vibration.Execute(0, 0.3f, 180f, 1, inputSource);
        }
    }

}
