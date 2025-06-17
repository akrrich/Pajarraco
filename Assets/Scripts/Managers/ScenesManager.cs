using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager
{
    public ScenesManager()
    {
        SetInitializedScene();
    }


    public void ChangeScene(string sceneName) 
    {
        LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    private void SetInitializedScene()
    {
        Scene initializedCurrentScene = SceneManager.GetActiveScene();

        switch (initializedCurrentScene.name)
        {
            case "MainMenu":
                LoadSceneAdditive("MainMenuUI");
                break;

            case "Level1":
                LoadSceneAdditive("Level1UI");
                break;
        }
    }

    private AsyncOperation LoadSceneAdditive(string sceneName)
    {
        return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    private AsyncOperation LoadScene(string sceneName)
    {
        return SceneManager.LoadSceneAsync(sceneName);
    }
}
