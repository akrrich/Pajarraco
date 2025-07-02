using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerView
{
    private PlayerModel playerModel;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private List<Image> lifes = new List<Image>();

    private static event Action onPlayerDeath;

    public static Action OnPlayerDeath { get => onPlayerDeath; set => onPlayerDeath = value; }


    public PlayerView(PlayerController playerController)
    {
        playerModel = playerController.PlayerModel;
        animator=playerController.GetComponentInChildren<Animator>();
        spriteRenderer = playerController.GetComponentInChildren<SpriteRenderer>();
        SuscribeToPlayerModelLifeEvent();
        playerController.StartCoroutine(FindHearts());
    }


    public void UnsuscribeToPlayerModelLifeEvent()
    {
        playerModel.OnUpdateLifesUI -= UpdateLifesUI;
    }


    private void SuscribeToPlayerModelLifeEvent()
    {
        playerModel.OnUpdateLifesUI += UpdateLifesUI;
    }

    private IEnumerator FindHearts()
    {
        yield return new WaitForSeconds(1);

        GameObject canvasPlayerLifes = GameObject.Find("CanvasPlayerLifes");

        foreach (Transform heart in canvasPlayerLifes.transform)
        {
            lifes.Add(heart.GetComponent<Image>());
        }

        UpdateLifesUI();
    }

    private void UpdateLifesUI()
    {
        int currentLifes = playerModel.Life;

        for (int i = 0; i < lifes.Count; i++)
        {
            lifes[i].enabled = i < currentLifes;
        }
    }
    public void UpdateAnimation(float speed,float dirX)
    {
        animator.SetFloat("Speed",Mathf.Abs(speed));
        if (dirX > 0.01f) spriteRenderer.flipX = false;
        else if (dirX < -0.01f) spriteRenderer.flipX = true;
    }
    
    public void PlayJump() => animator.SetTrigger("IsJumping");
}
