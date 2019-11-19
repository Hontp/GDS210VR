using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

public class MenuInteracter : MonoBehaviour
{
    public Graphic thingo;
    public Camera cam;
    public LayerMask WorldUILayer;
    public GameObject redDot;
    public Transform rayOrigin;

    public SteamVR_Input_Sources inputSource;
    public SteamVR_Action_Boolean grabPinch;

    RaycastHit hit;
    private void Update()
    {
        //if (Physics.Raycast(new Ray(rayOrigin.position, rayOrigin.up), out hit, 200, WorldUILayer))
        //{
        //    redDot.transform.position = hit.point;
        //}
        if (thingo.Raycast(cam.WorldToScreenPoint(rayOrigin.position), cam))
        {
            redDot.transform.position = hit.point;
        }
        //CheckShoot();
    }

    private void CheckShoot()
    {
        if (grabPinch.GetStateDown(inputSource))
        {
            if(Physics.Raycast(new Ray(rayOrigin.position , rayOrigin.up), out hit,200))
            {
                redDot.transform.position = hit.point;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + rayOrigin.up*100);
    }

}
