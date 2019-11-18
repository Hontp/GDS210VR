using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SwordHitting : MonoBehaviour
{
    public SteamVR_Action_Vibration vibration;
    public SteamVR_Input_Sources inputSource;
    public Hand hand;

    public void Start()
    {
        hand = GetComponentInParent<Hand>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            vibration.Execute(0f, 0.4f, 20f, 1, inputSource);
            hand.TriggerHapticPulse(180);

            other.gameObject.GetComponent<Enemy>().slice(other.GetContact(other.contactCount/2).point,transform.up);
        }
    }

}
