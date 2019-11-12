using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpawnEnemy : MonoBehaviour
{
    public Transform enemySpawn1, enemySpawn2, enemySpawn3;
    public GameObject respawnEnemy;
    // I believe in this curve
    public AnimationCurve waveCurve;
    public int currentWaveNumber =0;
    [SerializeField]
    [Header("Max Wave Number (Here for reference)")]
    public const int maxWaves = 20;
    public int[] enemyAmounts = new int[maxWaves];

    

    public void incWave()
    {
        currentWaveNumber++;
    }

    public int sampleWave(int waveNumber)
    {
        return (int)( waveCurve.Evaluate(0.05f * waveNumber) * 20f);
    }


    void Start()
    {
        for(int i=0;i<maxWaves;i++)
        {
            enemyAmounts[i] = sampleWave(i);
        }
    }

    public void TestSpawn()
    {
        int spawn = Random.Range(0, 3);
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
