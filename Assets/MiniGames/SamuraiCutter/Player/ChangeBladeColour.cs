using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBladeColour : MonoBehaviour
{
    public Material blade;
    public Vector4 blue, red, green, purple, yellow;
    // Start is called before the first frame update
    void Start()
    {
        blue = new Vector4(0, 150, 190, 1.5f);
        red = new Vector4(140, 0, 0, 1.5f);
        green = new Vector4(0, 70, 0, 1.5f);
        purple = new Vector4(50, 0, 255, 1.5f);
        yellow = new Vector4(150, 150, 20, 1.5f);

        blade.SetColor("_Emission", blue);
    }

    public void MakeBlue()
    {
        blade.SetColor("_Emission", blue);
    }

    public void MakePurple()
    {
        blade.SetColor("_Emission", purple);
    }

    public void MakeRed()
    {
        blade.SetColor("_Emission", red);
    }

    public void MakeYellow()
    {
        blade.SetColor("_Emission", yellow);
    }

    public void MakeGreen()
    {
        blade.SetColor("_Emission", green);
    }
}
