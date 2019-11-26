using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDrop
{

    public class game_maneger : MonoBehaviour
    {
        //0 menue, 1 game start, 2 gameplay, 3 game end
        public int game_phase=0;

        //ui refrence
        public MenuSystem UI;

        float player_time_survived;
        int minuts;
        int seconds;

        Vector2 player_score;
        public Vector2 high_score1;
        public Vector2 high_score2;
        public Vector2 high_score3;

        public Vector2[] highscores= new Vector2[3];
        // Start is called before the first frame update
        void Start()
        {
            game_phase = 0;
            UI.MenuActive();
            if (PlayerPrefs.HasKey("highsscore1_min"))
            {
                high_score1 = new Vector2(PlayerPrefs.GetInt("highsscore1_min"), PlayerPrefs.GetInt("highsscore1_sec"));
                high_score2 = new Vector2(PlayerPrefs.GetInt("highsscore2_min"), PlayerPrefs.GetInt("highsscore2_sec"));
                high_score3 = new Vector2(PlayerPrefs.GetInt("highsscore3_min"), PlayerPrefs.GetInt("highsscore3_sec"));
                highscores[0] = high_score1;
                highscores[1] = high_score2;
                highscores[2] = high_score3;
            }
        }

        // Update is called once per frame
        void Update()
        {

            highscores[0] = high_score1;
            highscores[1] = high_score2;
            highscores[2] = high_score3;

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                game_phase++;
            }
            if (game_phase == 0)
            {
                if (UI.gamePlaying == true)
                {
                    game_phase = 1;
                }
            }
            if (game_phase == 1)
            {
                
                player_time_survived = 0;
            }
            if (game_phase == 2)
            {
                player_time_survived += Time.deltaTime;
            }
            if (game_phase == 3)
            {
                player_time_survived = (int)Mathf.Round(player_time_survived);

                minuts = (int)player_time_survived / 60;
                seconds = (int)player_time_survived % 60;

                player_score = new Vector2(minuts, seconds);
                //comparing scores
                compare_scores();




                UI.MenuActive();
                game_phase = 0;

            }
        }
        void compare_scores()
        {
            int position = -1;
            for (int i = 0; highscores.Length > i; i++)
            {
                if (highscores[i].x < player_score.x)
                {
                    position++;
                    
                    for (int x = 1; i >= x; x++)
                    {
                        if (position == 1)
                        {
                            high_score1 = high_score2;
                        }
                        if (position == 2)
                        {
                            high_score2 = high_score3;
                        }
                    }

                }
                else if (highscores[i].x == player_score.x && highscores[i].y < player_score.y)
                {
                    position++;
                    for (int x = 1; i >= x; x++)
                    {

                        if (position == 1)
                        {
                            high_score1 = high_score2;
                        }
                        if (position == 2)
                        {
                            high_score2 = high_score3;
                        }
                    
                    }
                }
            }
            if (position == 0)
            {
                high_score3 = player_score;
            }
            if (position == 1)
            {
                high_score2 = player_score;
            }
            if (position == 2)
            {
                high_score3 = player_score;
            }
            

            PlayerPrefs.SetInt("highsscore1_min", (int)high_score1.x);
            PlayerPrefs.SetInt("highsscore1_sec", (int)high_score1.y);

            PlayerPrefs.SetInt("highsscore2_min", (int)high_score2.x);
            PlayerPrefs.SetInt("highsscore2_sec", (int)high_score2.y);

            PlayerPrefs.SetInt("highsscore3_min", (int)high_score3.x);
            PlayerPrefs.SetInt("highsscore3_sec", (int)high_score3.y);

            PlayerPrefs.Save();
        }
    }
    
}