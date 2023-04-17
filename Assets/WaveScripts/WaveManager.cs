using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public List<GameObject> possibleEnemies;
    public List<GameObject> spawningLocations;
    public List<GameObject> spawnedEnemies;
    public float currentWave = 1f;
    public TMP_Text waveText;
    private int waveTimeElapsed = 10000;
    public int totalWaves = 5;
    int waveNumber = 1;
    public int enemyNumber = 10;
    public int maxWaveDuration = 10;
    private int waveDuration;
    bool startingNewWave = false;



    private void Start()
    {
        waveDuration = maxWaveDuration;
        
        StartCoroutine(SpawnWave(waveNumber,enemyNumber , waveDuration));
        StartCoroutine(WaveTimer(waveDuration, waveNumber));


        
    }

    private void Update()
    {
        if (waveTimeElapsed == 0 &! startingNewWave)
        {
            startingNewWave = true;
            NextWave();
        }
    }

    public void NextWave()
    {
        waveTimeElapsed = maxWaveDuration;
        waveDuration = maxWaveDuration;
        waveNumber += 1;
        enemyNumber++;
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
        int pickedEnemy = Random.Range(0, possibleEnemies.Count);
        int pickSpawner = Random.Range(0, spawningLocations.Count);
        Instantiate(possibleEnemies[pickedEnemy], spawningLocations[pickSpawner].transform.position, Quaternion.identity);
        spawnedEnemies.Add(possibleEnemies[pickedEnemy]);
        Debug.Log(possibleEnemies[pickedEnemy]);
        Debug.Log("Spawned enemy " + possibleEnemies[pickedEnemy].name + " at spawner " + spawningLocations[pickSpawner].name);
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
