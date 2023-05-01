using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public List<GameObject> possibleEnemies;
    public List<GameObject> spawningLocations;
    public float currentWave = 1f;
    public TMP_Text waveText;
    private int waveTimeElapsed = 10000;
    public int totalWaves = 5;
    int waveNumber = 1;
    public int enemyNumber = 10;
    public int maxWaveDuration = 10;
    private int waveDuration;
    bool startingNewWave = false;
    public int spawnedEnemies = 0;
    public int deadEnemies = 0;


    private void Start()
    {
        waveDuration = maxWaveDuration;
        
        StartCoroutine(SpawnWave(waveNumber,enemyNumber , waveDuration));
        StartCoroutine(WaveTimer(waveDuration, waveNumber));


        
    }

    private void Update()
    {
        
        if (waveTimeElapsed == 0 && deadEnemies >= spawnedEnemies &! startingNewWave)
        {
            startingNewWave = true;
            NextWave();
        }
    }



    public void NextWave()
    {
        deadEnemies = 0;
        spawnedEnemies = 0;
        waveTimeElapsed = maxWaveDuration;
        waveDuration = maxWaveDuration;
        waveNumber += 1;
        enemyNumber += 3;
        StartCoroutine(SpawnWave(waveNumber, enemyNumber, waveDuration));
        StartCoroutine(WaveTimer(waveDuration, waveNumber));
        startingNewWave = false;
    }

    public IEnumerator SpawnWave(int waveNumber, int enemyNumber, float durationInSeconds)
    {
        
        Debug.Log("Spawned Wave " + waveNumber + " with " + enemyNumber + " enemies for " + durationInSeconds + " seconds");
        float timePerEnemy = durationInSeconds / enemyNumber;
        for (int currentEnemy = 0; currentEnemy < enemyNumber; currentEnemy++ )
        {
            yield return new WaitForSeconds(timePerEnemy);
            SpawnEnemy();
        }
        

    }
    public void SpawnEnemy()
    {
        spawnedEnemies++;
        int pickedEnemy = Random.Range(0, possibleEnemies.Count);
        int pickSpawner = Random.Range(0, spawningLocations.Count);
        Instantiate(possibleEnemies[pickedEnemy], spawningLocations[pickSpawner].transform.position, Quaternion.identity);
        //Debug.Log(possibleEnemies[pickedEnemy]);
        //Debug.Log("Spawned enemy " + possibleEnemies[pickedEnemy].name + " at spawner " + spawningLocations[pickSpawner].name);
    }

    public IEnumerator WaveTimer(int startTime, int waveNumber)
    {
        for (int waveTime = startTime; waveTime >= 0; waveTime--)
        {
            waveTimeElapsed = waveTime;
            waveText.text = new string("Wave " + waveNumber.ToString() + " : " + waveTime + "s");
            yield return new WaitForSeconds(1);
            waveText.text = new string("Wave " + waveNumber.ToString() + " : " + waveTime + "s");
            waveTimeElapsed = waveTime;

        }
    }



}
