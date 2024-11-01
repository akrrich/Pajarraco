using System;
using UnityEngine;

public class EnemyEvents : MonoBehaviour
{
    private static event Action onEnemyLifeChange;
    private static event Action onEnemyDeath;

    public static Action OnEnemyLifeChange { get => onEnemyLifeChange; set => onEnemyLifeChange = value; }
    public static Action OnEnemyDeath { get => onEnemyDeath; set => onEnemyDeath = value; }
}
