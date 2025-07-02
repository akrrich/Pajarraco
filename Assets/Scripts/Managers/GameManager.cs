using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private UpdateManager updateManager;
    private ScenesManager scenesManager;
    private PauseManager pauseManager; 

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private PoolerManager poolerManager;

    private bool sceneLevel1WasInitialized = false;

    public static GameManager Instance { get => instance; }

    public UpdateManager UpdateManager { get => updateManager; }
    public ScenesManager ScenesManager { get => scenesManager; }
    public AudioManager AudioManager { get => audioManager; }
    public PauseManager PauseManager { get => pauseManager; }
    public PoolerManager PoolerManager { get => poolerManager; }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        InitializeManagers();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (updateManager != null)
        {
            updateManager.Update();
        }
    }

    void OnDrawGizmos()
    {
        if (updateManager != null)
        {
            updateManager.DrawGizmos();
        }
    }


    private void InitializeManagers()
    {
        updateManager = new UpdateManager();
        scenesManager = new ScenesManager();
        pauseManager = new PauseManager();
        audioManager.Initialize();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level")
        {
            if (!sceneLevel1WasInitialized)
            {
                poolerManager.Initialize();
                sceneLevel1WasInitialized = true;
            }

            else
            {
                poolerManager.ReinitializeBulletsReferences();
            }
        }
    }
}
