using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static int round;

    [SerializeField]
    private GameObject[] enemyType;
    private int enemyGroup = 2;
    private int enemyAmount = 3;

    [SerializeField]
    private GameObject[] itemType;
    [SerializeField]
    private const int maxItems = 30;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private GameObject ground;
    [SerializeField]
    private const float itemSpawnY = 22;
    private const float itemSpawnShift = 5;
    private float groundScaleX;

    private bool roundCompleted = true;
    private bool enemiesAdded = false;
    private bool spawnItemsDuringRound = true;

    private float timer = 10;

    public GameObject gameOverCanvas;
    public GameObject scoreCanvas;

    void Start()
    {
        gameOverCanvas.SetActive(false);
        Time.timeScale = 1;
        groundScaleX = ground.transform.localScale.x;
    }

    void Update()
    {
        CheckEnemies();
    }

    private void CheckEnemies()
    {
        int countEnemies = 0;
        foreach (GameObject enemy in enemyType)
        {
            if (!GameObject.FindWithTag(enemy.tag))
            {
                countEnemies++;
            }
            if (enemyType.Length == countEnemies)
            {
                if (roundCompleted)
                {
                    if (!enemiesAdded)
                    {
                        enemiesAdded = AddEnemiesAmount();
                        int itemsOnScene = CheckItems();
                        if (itemsOnScene < maxItems)
                        {
                            int enemiesThisRound = enemyGroup * enemyAmount;
                            if (itemsOnScene < enemiesThisRound)
                            {
                                if (enemiesThisRound - itemsOnScene < maxItems)
                                {
                                    SpawnItemsBeforeRound(enemiesThisRound - itemsOnScene);
                                }
                                else SpawnItemsBeforeRound(maxItems - itemsOnScene);
                            }
                        }
                    }
                    if (timer > 0) timer -= Time.deltaTime;
                    if (timer < 0)
                    {
                        timer = 10;
                        StartCoroutine("SpawnEnemies");
                        roundCompleted = false;
                        round++;
                        Score.SCORE += 500;
                        break;
                    }
                }
            }
            else if (spawnItemsDuringRound && !roundCompleted)
            {
                StartCoroutine("SpawnItemsDuringRound");
            }
        }
    }

    private bool AddEnemiesAmount()
    {
        if (round % 2 == 0)
        {
            enemyGroup++;
        }
        else
        {
            enemyAmount += 2;
        }
        return true;
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 1; i <= enemyGroup; i++)
        {
            for (int j = 1; j <= enemyAmount; j++)
            {
                Transform spawn = spawnPoints[Random.Range(0, 2)];
                GameObject enemy = enemyType[Random.Range(0, 3)];
                if ((enemy.transform.localScale.x > 0 && spawn.tag == "SpawnRight")
                || (enemy.transform.localScale.x < 0 && spawn.tag == "SpawnLeft"))
                {
                    enemy = EnemyFlip(enemy);
                }
                Instantiate(enemy, spawn.position, spawn.rotation);
                if (j != enemyAmount) yield return new WaitForSeconds(Random.Range(0.7f, 1.1f));
            }
            if (i != enemyGroup) yield return new WaitForSeconds(5);
        }
        enemiesAdded = false;
        roundCompleted = true;
    }

    private int CheckItems()
    {
        int countItems = 0;
        foreach (GameObject item in itemType)
        {
            countItems += GameObject.FindGameObjectsWithTag(item.tag).Length;
        }
        return countItems;
    }

    private void SpawnItemsBeforeRound(int itemsToSpawn)
    {
        for (int i = 1; i <= itemsToSpawn; i++)
        {
            SpawnItems();
        }
    }

    IEnumerator SpawnItemsDuringRound()
    {
        spawnItemsDuringRound = false;
        int maxItems;
        if (CheckItems() < enemyGroup * enemyAmount / 2)
        {
            yield return new WaitForSeconds(5);
            maxItems = 10;
        }
        else
        {
            yield return new WaitForSeconds(5);
            maxItems = 4;
        }
        int items = Random.Range(1, maxItems);
        for (int i = 1; i <= items; i++)
        {
            SpawnItems();
        }
        spawnItemsDuringRound = true;
    }

    private void SpawnItems()
    {
        GameObject item = itemType[Random.Range(0, 3)];
        float itemSpawnX = Random.Range(-groundScaleX / 2 + itemSpawnShift, groundScaleX / 2 - itemSpawnShift);
        Vector3 spawn = new Vector3(itemSpawnX, itemSpawnY, 0);
        GameObject finallySpawned = Instantiate(item, spawn, new Quaternion(0, 0, 0, 0));
        finallySpawned.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10f, 10f),
            Random.Range(-2f, 2f)), ForceMode2D.Impulse);
    }

    private GameObject EnemyFlip(GameObject enemy)
    {
        Vector3 mainScale = enemy.transform.localScale;
        mainScale.x *= -1;
        enemy.transform.localScale = mainScale;
        return enemy;
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
        scoreCanvas.SetActive(false);
    }

    public void Replay()
    {
        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

}
