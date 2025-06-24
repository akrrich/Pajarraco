using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerView
{
    private PlayerModel playerModel;

    private List<Image> lifes = new List<Image>();

    private static event Action onPlayerDeath;


    public PlayerView(PlayerController playerController)
    {
        playerModel = playerController.PlayerModel;
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
}
