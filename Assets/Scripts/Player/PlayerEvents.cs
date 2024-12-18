using UnityEngine;
using System;

public class PlayerEvents : MonoBehaviour
{
    private static event Action onLifeChange;
    private static event Action onPlayerDefeated;
    private static event Action onPlayerRespawn;

    public static Action OnLifeChange { get => onLifeChange; set => onLifeChange = value; } 
    public static Action OnPlayerDefeated { get => onPlayerDefeated; set => onPlayerDefeated = value; }
    public static Action OnPlayerRespawn { get => onPlayerRespawn; set => onPlayerRespawn = value; }
}
