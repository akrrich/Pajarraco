using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    private Stack<ICommand> commandPowerUpsStack = new Stack<ICommand>();

    private Vector2 minSpawnRange = new Vector2(-7, 8.5f);
    private Vector2 maxSpawnRange = new Vector2(7, 6f);

    private int numberToGuess;


    void Awake()
    {
        numberToGuess = Random.Range(0, 700);

        GameManager.Instance.GameStatePlaying += PowerUpsManagerUpdate;
    }

    void PowerUpsManagerUpdate()
    {
        InstantiateRandomPowerUpInRandomPosition();
    }

    void OnDestroy()
    {
        GameManager.Instance.GameStatePlaying -= PowerUpsManagerUpdate;
    }


    public void AddPowerUp(ICommand command, float durationActualPowerUp)
    {
        command.Execute();
        commandPowerUpsStack.Push(command);

        StartCoroutine(HandlePowerUpDuration(command, durationActualPowerUp));
    }


    private void InstantiateRandomPowerUpInRandomPosition()
    {
        int randomNumbersGeneratedAllTime = Random.Range(0, 700);

        if (randomNumbersGeneratedAllTime == numberToGuess)
        {
            numberToGuess = Random.Range(0, 700);

            Vector2 randomPosition = new Vector2(

                Random.Range(minSpawnRange.x, maxSpawnRange.x),
                Random.Range(minSpawnRange.y, maxSpawnRange.y)
            );

            AbstractFactory.CreatePowerUp(Random.Range(0, 3), randomPosition);
        }
    }

    private IEnumerator HandlePowerUpDuration(ICommand command, float durationActualPowerUp)
    {
        yield return new WaitForSeconds(durationActualPowerUp);

        command.Undo();
        commandPowerUpsStack.Pop();
    }
}
