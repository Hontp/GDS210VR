using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

public class MenuInteracter : MonoBehaviour
{
    public MenuSystem menu;
    public LineRenderer laserPointer;
    public GameObject redDot;
    public Transform rayOrigin;

    public SteamVR_Input_Sources inputSource;
    public SteamVR_Action_Boolean grabPinch;

    RaycastHit hit;
    private void Update()
    {
        laserPointer.SetPosition(0, rayOrigin.position);
        laserPointer.SetPosition(1, rayOrigin.position + rayOrigin.up * 200);
        CheckShoot();
    }

    private void CheckShoot()
    {
        if (grabPinch.GetStateDown(inputSource))
        {
            if(Physics.Raycast(new Ray(rayOrigin.position , rayOrigin.up), out hit,200))
            {
                GameObject target = hit.transform.gameObject;
                redDot.transform.position = hit.point;
                if(target != null)
                {
                    if (target.CompareTag("PlayButton"))
                    {
                        menu.PlayGame();
                    }
                    if (target.CompareTag("DiffButton"))
                    {
                        menu.ChangeDifficulty();
                    }
                    if (target.CompareTag("ScoreButton"))
                    {
                        menu.OnScoreButtonPress();
                    }
                    if (target.CompareTag("HomeButton"))
                    {
                        menu.HomeButton();
                    }
                }   
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + rayOrigin.up*100);
    }

}
