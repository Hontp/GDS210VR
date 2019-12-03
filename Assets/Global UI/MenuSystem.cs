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
    TMP_Text difficultyTB;
    [SerializeField]
    TMP_Text scoreTitleTB;
    [SerializeField]
    GameObject difTB;
    [SerializeField]
    TMP_Text scoreTB;
    [SerializeField]
    GameObject mainCanvas;
    [SerializeField]
    GameObject scoreCanvas;
    [SerializeField]
    GameObject difPanel;

    string gameName;
    EnemySpawner spawner;
    int difficulty = 0;
    bool mainMenu = true;

    const string TITLESTART = "Welcome to ";
    const string SCOREEND = " High Scores";






    // Start is called before the first frame update
    void Awake()
    {
        DoOnStart();
    }

    void DoOnStart()
    {
        switch (myGame)
        {
            case GameLoaded.None:
                break;
            case GameLoaded.Sword:
                SetName("Samurai Cutter");
                
                difficultyTB.gameObject.SetActive(false);
                difTB.SetActive(false);
                difPanel.SetActive(false);
                break;
            case GameLoaded.Gun:
                SetName("Space Escape");
                spawner = FindObjectOfType<EnemySpawner>();
                spawner.SetSpawnVariables(30, 0.01f, 6, 1);
                difficultyTB.gameObject.SetActive(true);
                difTB.SetActive(true);
                difPanel.SetActive(true);
                break;
            case GameLoaded.Tower:
                SetName("Tower Game");
                difficultyTB.gameObject.SetActive(false);
                difTB.SetActive(false);
                difPanel.SetActive(false);
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
        mainMenu = false;
        mainCanvas.SetActive(false);
        scoreCanvas.SetActive(true);
        switch (myGame)
        {
            case GameLoaded.None:
                break;
            case GameLoaded.Sword:
                SamuraiCutter.GameManager Scgm = GameObject.Find("GameManager").GetComponent<SamuraiCutter.GameManager>();
                scoreTB.text = Scgm.GetComponent<Scoring>().scoreText;
                break;
            case GameLoaded.Gun:
                scoreTB.text = "John is cool dude, thanks for the UI Friend Much Appreciated";
                break;
            case GameLoaded.Tower:
                TowerDrop.game_maneger Tgm= GameObject.Find("GameScene").GetComponent<TowerDrop.game_maneger>();
                scoreTB.text = "high Score 1: " + Tgm.high_score1.ToString() +"\n"+ "high Score 2: " + Tgm.high_score2.ToString() + "\n"+"high Score 3: " + Tgm.high_score3.ToString();
                break;
        }
    }

    //runs when score back to menu button is pressed and goes back to the main menu
    public void BackToMain()
    {
        mainMenu = true;
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
        if(myGame == GameLoaded.Sword)
        {
            print("Running the game");
            SamuraiCutter.GameManager._instance.started = true;
            Time.timeScale = 1f;
            SamuraiCutter.GameManager._instance.spawnEnemy.currentWaveNumber = 0;
            SamuraiCutter.GameManager._instance.spawnEnemy.remainingEnemies = 0;
        }

        gameObject.SetActive(false);
        gamePlaying = true;
    }

    public void MenuActive()
    {
        print("reactivating Menu");
        gameObject.SetActive(true);
        gamePlaying = false;
    }

    public void ChangeDifficulty()
    {
        difficulty++;
        if(difficulty > 3)
        {
            difficulty = 0;
        }
        switch (difficulty)
        {
            case 0:
                difficultyTB.text = "Easy";
                spawner.SetSpawnVariables(30, 0.01f, 6, 1);
                break;
            case 1:
                difficultyTB.text = "Medium";
                spawner.SetSpawnVariables(50, 0.04f, 5, 2);
                break;
            case 2:
                difficultyTB.text = "Hard";
                spawner.SetSpawnVariables(75, 0.07f, 4, 3);
                break;
            case 3:
                difficultyTB.text = "Insane";
                spawner.SetSpawnVariables(300, 0.1f, 4, 4);
                break;
        }
    }

    public void HomeButton()
    {
        if (mainMenu)
        {
            ReturnToHub();
        }
        else
        {
            BackToMain();
        }
    }
}
