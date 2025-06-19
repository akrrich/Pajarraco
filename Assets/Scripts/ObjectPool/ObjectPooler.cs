using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Header("Pooling Config")]
    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private int poolSize;

    private List<Transform> playerPool = new List<Transform>();
    private List<Transform> enemyPool = new List<Transform>();
    private List<Bullet> activeBullets = new List<Bullet>();


    void Awake()
    {
        InitializePool(playerBulletPrefab, playerPool);
        InitializePool(enemyBulletPrefab, enemyPool);
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        for (int i = activeBullets.Count - 1; i >= 0; i--)
        {
            activeBullets[i].Tick(deltaTime);
        }
    }


    private void InitializePool(GameObject prefab, List<Transform> targetPool)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.transform.position = transform.position;
            obj.SetActive(false);
            targetPool.Add(obj.transform);
        }
    }

    private Transform GetObjectFromPool(List<Transform> pool)
    {
        foreach (var obj in pool)
        {
            if (!obj.gameObject.activeSelf)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        return null;
    }

    private void ReturnObjectToPool(Bullet bullet)
    {
        activeBullets.Remove(bullet);

        Transform t = bullet.GetTransform();
        t.SetParent(transform);
        t.gameObject.SetActive(false);
    }


    public void FireBullet(Vector3 position, Vector3 direction, float speed, bool isEnemyBullet = false)
    {
        List<Transform> selectedPool = isEnemyBullet ? enemyPool : playerPool;
        Transform bulletTransform = GetObjectFromPool(selectedPool);

        if (bulletTransform == null)
        {
            Debug.LogWarning("No hay balas disponibles en el pool seleccionado.");
            return;
        }

        bulletTransform.position = position;

        Bullet bullet = new Bullet(bulletTransform, ReturnObjectToPool);
        bullet.Init(direction, speed);
        activeBullets.Add(bullet);
    }
}
