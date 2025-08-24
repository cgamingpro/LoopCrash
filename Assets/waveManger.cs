using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveManger : MonoBehaviour
{
    [SerializeField]List<waveFormat> waveFormats = new List<waveFormat>();

    playerMovment playerMovment;
    trailSpawner trailSpawner;
    int currentWaveIndex = 0;

    private void Start()
    {
        playerMovment = GetComponent<playerMovment>();
        trailSpawner = GetComponent<trailSpawner>();
        ApplyWave(0);
    }
    private void Update()
    {
        if (playerMovment.score >= waveFormats[currentWaveIndex].totalEnemySpawn * 100  - 50)
        {
            currentWaveIndex++;
            playerMovment.score += currentWaveIndex * 500;
            ApplyWave(currentWaveIndex);
        }
    

    
    }
    void ApplyWave(int waveINdex)
    {
        trailSpawner.delay = waveFormats[waveINdex].delay;
        trailSpawner.maxTrail = waveFormats[waveINdex].totalEnemySpawn;
    }
}
