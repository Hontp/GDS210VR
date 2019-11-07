using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform enemySpawn1, enemySpawn2, enemySpawn3;
    public GameObject respawnEnemy;

    public void TestSpawn()
    {
        int spawn = Random.Range(0, 2);
        Transform currentTransform;
        switch (spawn)
        {
            case 0:
                currentTransform = enemySpawn1;
                break;
            case 1:
                currentTransform = enemySpawn2;
                break;

            case 2:
                currentTransform = enemySpawn3;
                break;
            default:
                currentTransform = enemySpawn1;
                break;
        }
        Instantiate(respawnEnemy, currentTransform.position, currentTransform.rotation);
    }
}
