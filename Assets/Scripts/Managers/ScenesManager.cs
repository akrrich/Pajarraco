using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

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

    private AsyncOperationHandle<SceneInstance> LoadSceneAdditive(string sceneAddress)
    {
        var handle = Addressables.LoadSceneAsync(sceneAddress, LoadSceneMode.Additive);
        handle.Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log($"[Addressables] Additive scene loaded: {sceneAddress}");
            }
            else
            {
                Debug.LogError($"[Addressables] Failed to load additive scene: {sceneAddress}");
            }
        };
        return handle;
    }

    private AsyncOperationHandle<SceneInstance> LoadScene(string sceneAddress)
    {
        var handle = Addressables.LoadSceneAsync(sceneAddress, LoadSceneMode.Single);
        handle.Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log($"[Addressables] Scene loaded: {sceneAddress}");
            }
            else
            {
                Debug.LogError($"[Addressables] Failed to load scene: {sceneAddress}");
            }
        };
        return handle;
    }
}
