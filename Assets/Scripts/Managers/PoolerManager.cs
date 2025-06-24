using System;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Player,
    Enemy
}

[Serializable]
public class PoolerManager
{
    [Header("Pooling Settings")]
    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private Transform poolParent;
    [SerializeField] private int poolSize;

    private List<BulletPlayer> playerBulletPool = new List<BulletPlayer>();
    private List<BulletEnemy> enemyBulletPool = new List<BulletEnemy>();


    public void Initialize()
    {
        InitializeBulletPool<BulletPlayer>(playerBulletPrefab, playerBulletPool, (t) => new BulletPlayer(t, ReturnObjectToPool));
        InitializeBulletPool<BulletEnemy>(enemyBulletPrefab, enemyBulletPool, (t) => new BulletEnemy(t, ReturnObjectToPool));
    }

    public void FireBullet(BulletType type, Transform origin, Vector3 direction)
    {
        switch (type)
        {
            case BulletType.Player:
                FireBullet(playerBulletPool, origin, direction);
                break;

            case BulletType.Enemy:
                FireBullet(enemyBulletPool, origin, direction);
                break;
        }
    }

    public void ReinitializeBulletsReferences()
    {
        foreach (var bullet in playerBulletPool)
        {
            bullet.ReinitializeSceneReferences();
        }

        foreach (var bullet in enemyBulletPool)
        {
            bullet.ReinitializeSceneReferences();
        }
    }

    public void ReturnAllBulletsToPool()
    {
        foreach (var bullet in playerBulletPool)
        {
            bullet.ReturnToPool();
        }

        foreach (var bullet in enemyBulletPool)
        {
            bullet.ReturnToPool();
        }
    }


    private void InitializeBulletPool<T>(GameObject prefab, List<T> pool, Func<Transform, T> createBullet) where T : Bullet
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = UnityEngine.Object.Instantiate(prefab, poolParent);
            obj.transform.position = poolParent.position;
            obj.SetActive(false);

            T bullet = createBullet(obj.transform);
            pool.Add(bullet);
        }
    }

    private void FireBullet<T>(List<T> pool, Transform origin, Vector3 direction) where T : Bullet
    {
        T bullet = GetBulletFromPool(pool);
        if (bullet != null)
        {
            bullet.Transform.position = origin.position;
            bullet.SetDir(direction);
        }
    }

    private T GetBulletFromPool<T>(List<T> pool) where T : Bullet
    {
        foreach (var bullet in pool)
        {
            if (!bullet.Transform.gameObject.activeSelf)
            {
                bullet.Transform.gameObject.SetActive(true);
                return bullet;
            }
        }

        return null;
    }

    private void ReturnObjectToPool(Bullet bullet)
    {
        Transform t = bullet.Transform;
        t.SetParent(poolParent);
        t.gameObject.SetActive(false);
    }
}
