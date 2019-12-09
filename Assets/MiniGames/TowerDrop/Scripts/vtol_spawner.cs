﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDrop
{
    public class vtol_spawner : MonoBehaviour
    {
        public Transform[] spawn_points;
        public GameObject vtol;
        public game_maneger gm;
        float spawn_tick;
        public float spawn_rate;

        // Start is called before the first frame update
        void Start()
        {
            gm = GetComponent<game_maneger>();
        }

        // Update is called once per frame
        void Update()
        {
            if (gm.game_phase == 2)
            {
                spawn_tick += Time.deltaTime;
                if (spawn_tick >= spawn_rate)
                {
                    spawn_tick = 0;
                    Instantiate(vtol, spawn_points[Random.Range(0, spawn_points.Length)]);
                }
            }
        }
    }
}
