using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private UpdateManager updateManager;
    private ScenesManager scenesManager;
    private PauseManager pauseManager;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private PoolerManager poolerManager;

    public static GameManager Instance { get => instance; }

    public UpdateManager UpdateManager { get => updateManager; }
    public ScenesManager ScenesManager { get => scenesManager; }
    public AudioManager AudioManager { get => audioManager; }
    public PauseManager PauseManager { get => pauseManager; }
    public PoolerManager PoolerManager { get => poolerManager; }


    void Awake()
    {
        CreateSingleton();
        InitializeManagers();
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


    private void CreateSingleton()
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
    }

    private void InitializeManagers()
    {
        updateManager = new UpdateManager();
        scenesManager = new ScenesManager();
        pauseManager = new PauseManager(); // = new PauseManager();
        audioManager.Initialize();
        poolerManager.Initialize();
    }
}
