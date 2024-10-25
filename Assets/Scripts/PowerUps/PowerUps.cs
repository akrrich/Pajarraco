using UnityEngine;

public abstract class PowerUps : MonoBehaviour
{
    [SerializeField] private int id;

    public int Id { get => id; }


    protected abstract void ActivePowerUp();
}
