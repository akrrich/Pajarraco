using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    public static MusicManager Instance { get =>  instance; }

    private AudioSource[] musicsManager; // [0] = MainMenu, [1] = PlayinGame, [2] = Win, [3] = Lose, [4] = Credits

    private int currentMusicIndex = -1;

    private bool[] musicBooleansActive = { false, false, false, false, false };


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
    }

    void Start()
    {
        musicsManager = GetComponents<AudioSource>();

        GameManager.Instance.GameStateMenu += () => PlayMusic(0);
        GameManager.Instance.GameStatePlaying += () => PlayMusic(1);
        GameManager.Instance.GameStateWin += () => PlayMusic(2);
        GameManager.Instance.GameStateTotalDefeated += () => PlayMusic(3);
        GameManager.Instance.GameStateCredits += () => PlayMusic(4);
    }

    private void PlayMusic(int musicIndex)
    {
        if (currentMusicIndex == musicIndex && musicsManager[musicIndex].isPlaying)
        {
            return;
        }

        StopAllMusic();

        musicsManager[musicIndex].playOnAwake = true;
        musicsManager[musicIndex].loop = (musicIndex != 2 && musicIndex != 3);
        musicsManager[musicIndex].Play();
        currentMusicIndex = musicIndex;
    }

    private void StopAllMusic()
    {
        for (int i = 0; i < musicsManager.Length; i++)
        {
            if (musicsManager[i].isPlaying)
            {
                musicsManager[i].Stop();
                musicBooleansActive[i] = false;
            }
        }
    }
}
