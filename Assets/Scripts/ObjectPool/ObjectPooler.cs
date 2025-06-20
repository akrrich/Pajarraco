using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Header("Pooling Config")]
    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private Transform parent;
    [SerializeField] private int poolSize;

    private List<Bullet> playerPool = new List<Bullet>();
    private List<Bullet> enemyPool = new List<Bullet>();


    void Awake()
    {
        InitializePool(playerBulletPrefab, playerPool);
        InitializePool(enemyBulletPrefab, enemyPool);
    }


    private void InitializePool(GameObject prefab, List<Bullet> targetPool)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, parent);
            obj.transform.position = parent.position;
            obj.SetActive(false);

            Bullet bullet = new Bullet(obj.transform, ReturnObjectToPool);
            targetPool.Add(bullet);
        }
    }

    private Bullet GetObjectFromPool(List<Bullet> pool)
    {
        foreach (var bullet in pool)
        {
            if (!bullet.GetTransform().gameObject.activeSelf)
            {
                bullet.GetTransform().gameObject.SetActive(true);
                return bullet;
            }
        }

        return null;
    }

    private void ReturnObjectToPool(Bullet bullet)
    {
        Transform t = bullet.GetTransform();
        t.SetParent(parent);
        t.gameObject.SetActive(false);
    }


    public void FireBullet(Vector3 position, Vector3 direction, bool isEnemyBullet = false)
    {
        List<Bullet> selectedPool = isEnemyBullet ? enemyPool : playerPool;
        Bullet bullet = GetObjectFromPool(selectedPool);

        if (bullet != null)
        {
            Transform t = bullet.GetTransform();
            t.position = position;
            bullet.Position = position;
            bullet.Init(direction);
        }
    }
}
