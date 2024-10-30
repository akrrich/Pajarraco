using UnityEngine;

public class PlayerMemento
{
    private Player player;

    private Vector2 mementoPosition;
    private int mementoLife = 3;

    public PlayerMemento(Player player)
    {
        this.player = player;
        SaveState();
    }   

    private void SaveState()
    {
        mementoPosition = player.transform.position;
    }

    public void RestoreState()
    {
        player.transform.position = mementoPosition;
        player.Life = mementoLife;
        player.gameObject.SetActive(true);

        PlayerEvents.OnLifeChange?.Invoke();
    }
}
