using System.Collections.Generic;
using UnityEngine;

public class AbstractFactory : MonoBehaviour
{
    [SerializeField] private PowerUps[] powerUps;
    private static Dictionary<int, PowerUps> idPowerUps = new Dictionary<int, PowerUps>();


    void Awake()
    {
        foreach (var powerUps in powerUps)
        {
            idPowerUps.Add(powerUps.Id, powerUps);
        }
    }

    void OnDestroy()
    {
        idPowerUps.Clear();
    }


    public static PowerUps CreatePowerUp(int id, Vector2 powerUpPosition)
    {
        if (!idPowerUps.TryGetValue(id, out PowerUps powerUps))
        {
            return null;
        }

        return Instantiate(powerUps, powerUpPosition, Quaternion.identity);
    }
}
