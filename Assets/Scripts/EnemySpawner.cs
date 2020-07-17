using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;

    public GameObject spawnLeft, spawnRight, spawnLeftObject, spawnRightObject;
    float enemyTimer, enemySpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        enemyTimer = 0;
        enemySpawnTime = Random.Range(3, 7);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.hasGameEnded || !GameController.Instance.hasGameStarted) return;

        if (spawnRightObject == null)
        {
            GameController.Instance.spawnRightTaken = false;
        }
        if (spawnLeftObject == null)
        {
            GameController.Instance.spawnLeftTaken = false;
        }

        enemyTimer += Time.deltaTime; 
        if (enemyTimer >= enemySpawnTime)
        {
            Debug.Log("Trying to spawn an enemy");
            SpawnEnemy();
            enemyTimer = 0;
            enemySpawnTime = Random.Range(1, 3);
        }
    }
    void SpawnEnemy()
    {
        if (GameController.Instance.isFinnSpawned)
        {
            if (Random.value > 0.5) return;
        }

        if (Random.value < 0.5 && !GameController.Instance.spawnLeftTaken)
        {
            GameObject enemyPrefab = enemies[Random.Range(0, enemies.Length-1)];
            GameObject enemy = Instantiate(enemyPrefab);
            //enemy.transform.SetParent(ElementsContainer.transform);
            EnemyController enemyController = enemy.GetComponent<EnemyController>();

            if (!GameController.Instance.spawnLeftTaken)
            {
                spawnLeftObject = enemy;
                GameController.Instance.spawnLeftTaken = true;
                enemy.transform.position = spawnLeft.transform.position;
                enemy.transform.eulerAngles += new Vector3(0, 0, -20);
                enemyController.type = "left";
                enemyController.originalPosition = spawnLeft.transform.position;
                enemyController.Activate();
                Debug.Log("Spawning left enemy");
            }
        }

        else if (Random.value >= 0.5 && !GameController.Instance.spawnRightTaken)
        {
            GameObject enemyPrefab = enemies[Random.Range(0, enemies.Length - 1)];
            GameObject enemy = Instantiate(enemyPrefab);
            //enemy.transform.SetParent(ElementsContainer.transform);
            EnemyController enemyController = enemy.GetComponent<EnemyController>();

            spawnRightObject = enemy;
            GameController.Instance.spawnRightTaken = true;
            enemy.transform.position = spawnRight.transform.position;
            enemy.transform.eulerAngles += new Vector3(0, 0, 20);
            enemyController.type = "right";
            enemyController.originalPosition = spawnRight.transform.position;
            enemyController.Activate();
            Debug.Log("Spawning right enemy");
        }
    }
}
