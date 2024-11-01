using UnityEngine;
using System.Collections;

public class PlayerMemento
{
    private Player player;
    private MonoBehaviour coroutineStarter;

    private Vector2 position;
    private int life = 3;

    private float durationBlinkEffect = 2.5f;
    private float intervalBlinkEffect = 0.1f;


    public PlayerMemento(Player player, MonoBehaviour coroutineStarter)
    {
        this.player = player;
        this.coroutineStarter = coroutineStarter;
        SaveState();
    }   


    public void RestoreState()
    {
        player.PlayerAudios[2].Play();
        player.transform.position = position;

        player.EnabledOrDisablePlayer(RigidbodyType2D.Dynamic, true, true);
        coroutineStarter.StartCoroutine(BlinkEffect(durationBlinkEffect, intervalBlinkEffect));

        PlayerEvents.OnLifeChange?.Invoke();
    }


    private void SaveState()
    {
        position = player.transform.position;
    }

    private IEnumerator BlinkEffect(float duration, float blinkInterval)
    {
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            player.SpriteRenderer.enabled = !player.SpriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }

        player.SpriteRenderer.enabled = true;
    }
}
