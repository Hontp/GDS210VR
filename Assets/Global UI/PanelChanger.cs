using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PanelChanger : MonoBehaviour
{
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
    }
    public void Selected()
    {
        ChangeTextColour(selected);
        timer = 0.1f;
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
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }







}
