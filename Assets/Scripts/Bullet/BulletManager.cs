using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private List<Bullet> activeBullets = new List<Bullet>();

    public void AddBullet(Bullet bullet)
    {
        activeBullets.Add(bullet);
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        for (int i = activeBullets.Count - 1; i >= 0; i--)
        {
            Bullet bullet = activeBullets[i];
            bullet.Tick(deltaTime);

            // Podés agregar lógica para destruir la bala, por ejemplo:
            if (bullet.Position.magnitude > 100f)
            {
                // Reusar desde el pool, o eliminar
                activeBullets.RemoveAt(i);
            }
        }
    }
}
