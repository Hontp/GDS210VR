using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PanelChanger : MonoBehaviour
{
    public enum State {Normal, Hovering, Selected };
    public State myState = State.Normal;

    public Color normal;
    public Color hover;
    public Color selected;
    [SerializeField]
    Image myimage;

    float timer = 0;


    public void ChangeTextColour(Color newColor)
    {
        myimage.color = newColor;
    }

    public void Hovering()
    {
        ChangeTextColour(hover);
        timer = 0.1f;
        myState = State.Hovering;
    }
    public void Selected()
    {
        ChangeTextColour(selected);
        timer = 0.1f;
        myState = State.Selected;
    }

    void Update()
    {
        resetColour();
    }

    void resetColour()
    {
        if(timer > 0)
        {
            ChangeTextColour(normal);
            myState = State.Normal;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }







}
