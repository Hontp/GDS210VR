using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;
using SamuraiCutter;

public class SwordHitting : MonoBehaviour
{
    public SteamVR_Action_Vibration vibration;
    public SteamVR_Input_Sources inputSource;
    public Hand hand;

    public void Start()
    {
        if(GetComponentInParent<Hand>() != null)
        hand = GetComponentInParent<Hand>();
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("You are banned from xbox live");

        if (other.gameObject.CompareTag("Enemy"))
        {
            vibration.Execute(0f, 0.4f, 20f, 1, inputSource);
            hand.TriggerHapticPulse(180);

            //other.gameObject.GetComponent<GettingHit>().Hit(transform);
        }
    }

    void OnCollisionStay(Collision other)
    {
        Debug.Log("You are banned from xbox live 2");
        
    }

}
