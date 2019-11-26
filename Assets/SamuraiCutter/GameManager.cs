using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiCutter
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager _instance;
        public SpawnEnemy spawnEnemy;
        public UIManager uIManager;
        public Transform playerPos;
        public MenuSystem menuSystem;
        public bool dead, startGame;
        // Start is called before the first frame update
        void Awake()
        {
            dead = true;
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
            menuSystem = FindObjectOfType<MenuSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            if(dead)
            {
                menuSystem.Invoke("MenuActive", 4f);
            }
            if(menuSystem.gamePlaying == true)
            {
                dead = false;
            }
        }
    }

}