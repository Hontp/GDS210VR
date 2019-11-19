using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using MemeMachine;

public class MenuSystem : MonoBehaviour
{
    public enum GameLoaded {None, Sword, Gun, Tower}
    public GameLoaded myGame = GameLoaded.None;
    public bool gamePlaying;

    [SerializeField]
    TMP_Text mainTitleTB;
    [SerializeField]
    TMP_Text scoreTitleTB;
    [SerializeField]
    GameObject difTB, difDrop;
    [SerializeField]
    TMP_Text scoreTB;
    [SerializeField]
    GameObject mainCanvas;
    [SerializeField]
    GameObject scoreCanvas;

    string gameName;
    EnemySpawner spawner;

    const string TITLESTART = "Welcome to ";
    const string SCOREEND = " High Scores";






    // Start is called before the first frame update
    void Start()
    {
        DoOnLoad();
    }

    void DoOnLoad()
    {
        switch (myGame)
        {
            case GameLoaded.None:
                break;
            case GameLoaded.Sword:
                SetName("Sword Game");
                difDrop.SetActive(false);
                difTB.SetActive(false);
                break;
            case GameLoaded.Gun:
                SetName("Space Escape");
                spawner = FindObjectOfType<EnemySpawner>();
                spawner.SetSpawnVariables(30, 0.01f, 6);
                difDrop.SetActive(true);
                difTB.SetActive(true);
                break;
            case GameLoaded.Tower:
                SetName("Tower Game");
                difDrop.SetActive(false);
                difTB.SetActive(false);
                break;
        }
    }

    //this fucntion gets given the name of the game and changes the relative text
    void SetName(string NewName)
    {
        gameName = NewName;
        mainTitleTB.text = TITLESTART + gameName;
        scoreTitleTB.text = gameName + SCOREEND;
    }

    // this fucntion runbs when the score button is clicked and sets the score textbox infomation
    public void OnScoreButtonPress()
    {
        mainCanvas.SetActive(false);
        scoreCanvas.SetActive(true);
        switch (myGame)
        {
            case GameLoaded.None:
                break;
            case GameLoaded.Sword:
                scoreTB.text = "your all noobs";
                break;
            case GameLoaded.Gun:
                scoreTB.text = "your all noobs";
                break;
            case GameLoaded.Tower:
                scoreTB.text = "your all noobs";
                break;
        }
    }


    //runs when score back to menu button is pressed and goes back to the main menu
    public void BackToMain()
    {
        mainCanvas.SetActive(true);
        scoreCanvas.SetActive(false);
    }

    public void ReturnToHub()
    {
        //Return to hub code here
    }

    //sets a bool to true for game to start
    public void PlayGame()
    {
        gameObject.SetActive(false);
        gamePlaying = true;
    } 
    
    public void MenuActive()
    {
        gameObject.SetActive(true);
        gamePlaying = true;
    }

    public void OnDifficultyChanged(int diff)
    {
        switch (diff)
        {
            case 0:
                spawner.SetSpawnVariables(30, 0.01f, 6);
                break;
            case 1:
                spawner.SetSpawnVariables(50, 0.04f, 5);
                break;
            case 2:
                spawner.SetSpawnVariables(75, 0.07f, 4);
                break;
            case 3:
                spawner.SetSpawnVariables(300, 0.1f, 4);
                break;
        }
    }
}
