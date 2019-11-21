using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

public class MenuInteracter : MonoBehaviour
{
    public MenuSystem menu;
    public LineRenderer laserPointer;
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
    private void Start()
    {
        menu = FindObjectOfType<MenuSystem>();
    }


    private void CheckShoot()
    {
        if (grabPinch.GetStateDown(inputSource))
        {
            if (Physics.Raycast(new Ray(rayOrigin.position, rayOrigin.up), out hit, 200))
            {
                GameObject target = hit.transform.gameObject;

                if (target != null)
                {
                    if (target.CompareTag("PlayButton"))
                    {
                        print("menu");
                        menu.Invoke("PlayGame", 0.15f);
                        target.GetComponent<PanelChanger>().Selected();
                    }
                    if (target.CompareTag("DiffButton"))
                    {
                        print("dif");
                        menu.Invoke("ChangeDifficulty", 0.15f);
                        target.GetComponent<PanelChanger>().Selected();
                    }
                    if (target.CompareTag("ScoreButton"))
                    {
                        print("score");
                        menu.Invoke("OnScoreButtonPress", 0.15f);
                        target.GetComponent<PanelChanger>().Selected();
                    }
                    if (target.CompareTag("HomeButton"))
                    {
                        print("home");
                        menu.Invoke("HomeButton", 0.15f);
                        target.GetComponent<PanelChanger>().Selected();
                    }
                }
            }
        }
        else
        {
            if (Physics.Raycast(new Ray(rayOrigin.position, rayOrigin.up), out hit, 200))
            {
                GameObject target = hit.transform.gameObject;
                if (target.CompareTag("PlayButton") || target.CompareTag("DiffButton") || target.CompareTag("ScoreButton") || target.CompareTag("HomeButton"))
                {
                    target.GetComponent<PanelChanger>().Hovering();
                }
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + rayOrigin.up*100);
    }

}
