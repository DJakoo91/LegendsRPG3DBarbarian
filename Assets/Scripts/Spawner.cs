using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject WinRing;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float waveDelay = 2f;

    private int faze = 0;
    private int aliveEnemies = 1;

    public LevelLoader LevelLoader;

    public void Start()
    {
        WinRing.SetActive(false);
    }
    
    public void EnemyDied()
    {
        aliveEnemies--;

        if (aliveEnemies == 0)
        {
            faze++;

            if (faze == 1)
                StartCoroutine(SpawnEnemiesWithDelay(3));
            else if (faze == 2)
                StartCoroutine(SpawnEnemiesWithDelay(2));
            else
                OnAllWavesComplete();
        }
    }

    private IEnumerator SpawnEnemiesWithDelay(int count)
    {
        yield return new WaitForSeconds(waveDelay);
        SpawnEnemies(count);
    }

    private void SpawnEnemies(int count)
    {
        aliveEnemies = count;

        for (int i = 0; i < count; i++)
        {
            Transform spawnPoint = spawnPoints[i % spawnPoints.Length];
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            EnemyHealth eh = newEnemy.GetComponent<EnemyHealth>();
            if (eh != null)
                eh.spawner = this;

            EnemyNavigation nav = newEnemy.GetComponent<EnemyNavigation>();
            if (nav != null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                    nav.SetTarget(player.transform);
            }
        }
    }

    private void OnAllWavesComplete()
    {
        WinRing.SetActive(true);   
    }
}
