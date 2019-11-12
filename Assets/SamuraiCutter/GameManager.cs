using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiCutter
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager _instance;
        public SpawnEnemy spawnEnemy;
        public Transform playerPos;
        // Start is called before the first frame update
        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }

}