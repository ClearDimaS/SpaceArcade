using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [Tooltip("Class controlling game state")]
    [SerializeField] private GameController gameController;

    [Tooltip("Bullets settings List")]
    [SerializeField] private BulletDataBase bulletSettings;

    [Tooltip("Objects ammount in the pool")]
    [SerializeField] private int poolCount;

    [Tooltip("BulletBase prefab")]
    [SerializeField] private GameObject bulletPrefab;

    [Tooltip("Time between the sapwns")]
    [SerializeField] private float spawnDeltaTime;

    [Tooltip("Point of shooting")]
    [SerializeField] private Transform shootingPoint;

    [Tooltip("Player controls class")]
    [SerializeField] private PlayerControls playerControls;

    public static Dictionary<GameObject, Bullet> Bullets;
    private Queue<GameObject> currentBullets;

    private void Start()
    {
        Bullets = new Dictionary<GameObject, Bullet>();
        currentBullets = new Queue<GameObject>();

        for (int i = 0; i < poolCount; i++)
        {
            InstantiateBullet();
        }

        Bullet.OnBulletOutOfBounds += ReturnBullet;
        gameController.OnGameOver += ReturnAll;
        StartCoroutine(Spawn());
    }

    private void OnEnable()
    {
        if (Bullets != null) 
        {
            StartCoroutine(Spawn());
        }
    }

    private void InstantiateBullet() 
    {
        GameObject prefab = Instantiate(bulletPrefab);
        prefab.transform.SetParent(transform.parent.transform.parent.transform);
        prefab.transform.localScale = new Vector3(1, 1, 1);
        Bullet script = prefab.GetComponent<Bullet>();
        prefab.SetActive(false);
        Bullets.Add(prefab, script);
        currentBullets.Enqueue(prefab);
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
            if (currentBullets.Count > 0 && playerControls.IsFirePressed())
            {
                // Enemy initialisation
                GameObject bullet = currentBullets.Dequeue();
                Bullet script = Bullets[bullet];
                bullet.SetActive(true);

                // Choosing random EnemyData
                script.Init(bulletSettings.GetRandomElement());

                // Generation
                bullet.transform.rotation = shootingPoint.transform.rotation;
                bullet.transform.position = new Vector2(shootingPoint.position.x, shootingPoint.position.y);
            }
        }
    }

    private void ReturnBullet(GameObject bullet)
    {
        bullet.transform.position = transform.position;
        bullet.SetActive(false);
        currentBullets.Enqueue(bullet);
    }

    private void ReturnAll()
    {
        foreach (KeyValuePair<GameObject, Bullet> keyValuePair in Bullets)
        {
            ReturnBullet(keyValuePair.Key);
        }
    }
}
