using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuSystem : MonoBehaviour
{
    public enum GameLoaded {None, Sword, Gun, Tower}
    public GameLoaded myGame = GameLoaded.None;


    [SerializeField]
    TMP_Text MainTitleTB;
    [SerializeField]
    TMP_Text ScoreTitleTB;
    //[SerializeField]
    //TMP_Text MainTitleTB;


    const string TITLESTART = "Welcome to ";






    // Start is called before the first frame update
    void Start()
    {
        DoOnLoad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DoOnLoad()
    {
        switch (myGame)
        {
            case GameLoaded.None:
                break;
            case GameLoaded.Sword:
                break;
            case GameLoaded.Gun:
                MainTitleTB.text = TITLESTART + "Space Escape"; 

                break;
            case GameLoaded.Tower:
                break;
        }

    }











}
