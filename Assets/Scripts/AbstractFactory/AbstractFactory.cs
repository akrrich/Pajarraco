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


    public static PowerUps CreatePowerUp(int id, Transform powerUpTransform)
    {
        if (!idPowerUps.TryGetValue(id, out PowerUps powerUps))
        {
            return null;
        }

        return Instantiate(powerUps, powerUpTransform.position, Quaternion.identity);
    }
}
