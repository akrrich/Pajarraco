using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private PlayerBullet bulletPrefab;

    private List<PlayerBullet> bulletPool = new List<PlayerBullet>();

    private int initialPoolSize = 15;
    private int currentBulletIndex = 0;


    void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            PlayerBullet bulletInstance = Instantiate(bulletPrefab);
            bulletInstance.gameObject.SetActive(false);
            bulletPool.Add(bulletInstance);
        }
    }

    public PlayerBullet GetBullet()
    {
        PlayerBullet bullet = null;

        for (int i = 0; i < bulletPool.Count; i++)
        {
            int index = (currentBulletIndex + i) % bulletPool.Count;

            if (!bulletPool[index].gameObject.activeInHierarchy)
            {
                bullet = bulletPool[index];
                bullet.gameObject.SetActive(true);

                currentBulletIndex = (index + 1) % bulletPool.Count;
                break;
            }
        }

        return bullet;
    }

    public void ReturnBulletToPool(PlayerBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
}
