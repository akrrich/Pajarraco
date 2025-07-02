using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;


    void Awake()
    {
        StartCoroutine(InitializeValues());
    }

    public void SetMusicVolume()
    {
        if (musicSlider.value <= 0.01f)
        {
            GameManager.Instance.AudioManager.SetMusicVolume(-80);
        }

        else
        {
            GameManager.Instance.AudioManager.SetMusicVolume(musicSlider.value);
        }

        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SetSFXVolume()
    {
        if (sfxSlider.value <= 0.01f)
        {
            GameManager.Instance.AudioManager.SetSFXVolume(-80);
        }

        else
        {
            GameManager.Instance.AudioManager.SetSFXVolume(sfxSlider.value);
        }

        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }


    private IEnumerator InitializeValues()
    {
        yield return new WaitForSeconds(1f);

        float defaultValue = 0.5f;

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", defaultValue);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", defaultValue);

        SetMusicVolume();
        SetSFXVolume();
    }
}
