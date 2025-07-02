using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager
{
    public ScenesManager()
    {
        SetInitializedScene();
    }


    public void ChangeScene(string sceneName, string additiveSceneName) 
    {
        LoadScene(sceneName);
        LoadSceneAdditive(additiveSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    private void SetInitializedScene()
    {
        LoadSceneAdditive("MainMenuUI");
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
