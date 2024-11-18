using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpName", menuName = "Flyweight/PowerUpData")]

public class PowerUpScriptable : ScriptableObject
{
    [SerializeField] private float durationTimePowerUp;
    private float speed = -3f;
    private float limitDownPowerUp = -3.425f;
    private float lifeTime = 2.25f;

    public float DurationTimePowerUp { get => durationTimePowerUp;}
    public float Speed { get =>  speed;}
    public float LimitDownPowerUp { get => limitDownPowerUp;}
    public float LifeTime { get => lifeTime;}
}
