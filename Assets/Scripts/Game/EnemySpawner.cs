using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Class controlling game state")]
    [SerializeField] private GameController gameController;

    [Tooltip("Enemies settings List")]
    [SerializeField] private EnemyDataBase enemySettings;

    [Tooltip("Objects ammount in the pool")]
    [SerializeField] private int poolCount;

    [Tooltip("EnemieBase prefab")]
    [SerializeField] private GameObject enemiePrefab;

    [Tooltip("Time between the sapwns")]
    [SerializeField] private float spawnDeltaTime;

    public static Dictionary<GameObject, Enemy> Enemies;
    private Queue<GameObject> currentEnemies;

    private void Start()
    {
        Enemies = new Dictionary<GameObject, Enemy>();
        currentEnemies = new Queue<GameObject>();

        for (int i = 0; i < poolCount; i++)
        {
            InstantiateEnemy();
        }

        Enemy.OnEnemyOutOfBounds += ReturnEnemy;
        gameController.OnGameOver += ReturnAll;
        StartCoroutine(Spawn());
    }

    private void OnEnable()
    {
        if (Enemies != null)
        {
            StartCoroutine(Spawn());
        }
    }

    private void InstantiateEnemy()
    {
        GameObject prefab = Instantiate(enemiePrefab);
        prefab.transform.SetParent(transform);
        prefab.transform.localScale = new Vector3(1, 1, 1);
        Enemy script = prefab.GetComponent<Enemy>();
        prefab.SetActive(false);
        Enemies.Add(prefab, script);
        currentEnemies.Enqueue(prefab);
    }

    private IEnumerator Spawn() 
    {
        if (spawnDeltaTime == 0) 
        {
            Debug.LogWarning($"spawnDeltaTime is not specified so using default value 1 s.");
            spawnDeltaTime = 1;
        }
        while (true) 
        {
            yield return new WaitForSeconds(spawnDeltaTime);
            if (currentEnemies.Count > 0) 
            {
                // Enemy initialisation
                GameObject enemy = currentEnemies.Dequeue();
                Enemy script = Enemies[enemy];
                enemy.SetActive(true);

                // Choosing random EnemyData
                script.Init(enemySettings.GetRandomElement());

                // Generation
                float yPos;
                float xPos;

                if (Random.Range(0.0f, 1.0f) < 0.5f)
                {
                    int randInd = Random.Range(0, 2);
                    xPos = ScreenViewManager.instance.screenBorders[randInd];
                    yPos = Random.Range(ScreenViewManager.instance.screenBorders[2], ScreenViewManager.instance.screenBorders[3]);
                    enemy.transform.rotation = Quaternion.Euler(0, 0, 180 * (randInd) + Random.Range(20, 160.0f));
                }
                else
                {
                    int randInd = Random.Range(2, 4);
                    yPos = ScreenViewManager.instance.screenBorders[randInd];
                    xPos = Random.Range(ScreenViewManager.instance.screenBorders[0], ScreenViewManager.instance.screenBorders[1]);
                    enemy.transform.rotation = Quaternion.Euler(0, 0, (randInd - 3) * 180 + Random.Range(-70f, 70.0f));
                }

                enemy.transform.position = new Vector2(xPos, yPos);
            }
        }
    }

    private void ReturnEnemy(GameObject _enemy) 
    {
        _enemy.transform.position = transform.position;
        _enemy.SetActive(false);
        currentEnemies.Enqueue(_enemy);
    }

    private void ReturnAll()
    {
        foreach (KeyValuePair<GameObject, Enemy> keyValuePair in Enemies)
        {
            ReturnEnemy(keyValuePair.Key);
        }
    }
}
