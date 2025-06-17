using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class AudioManager
{
    [Header("Mixer Groups")]
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips")]
    [SerializeField] private Sound[] musicClips; // Lista de música con nombre
    [SerializeField] private Sound[] sfxClips;   // Lista de SFX con nombre

    private Dictionary<string, AudioClip> musicDictionary = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxDictionary = new Dictionary<string, AudioClip>();


    public void Initialize()
    {
        InitializeAudiosDictionaries(musicDictionary, musicClips);
        InitializeAudiosDictionaries(sfxDictionary, sfxClips);
    }

    public void PlayMusic(string musicName)
    {
        if (!musicDictionary.ContainsKey(musicName))
        {
            Debug.LogWarning("Música no encontrada: " + musicName);
            return;
        }

        musicSource.clip = musicDictionary[musicName];
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(string sfxName)
    {
        if (!sfxDictionary.ContainsKey(sfxName))
        {
            Debug.LogWarning("SFX no encontrado: " + sfxName);
            return;
        }

        sfxSource.clip = sfxDictionary[sfxName];
        sfxSource.PlayOneShot(sfxSource.clip);
    }

    public float returnAudioLength(string sfxName)
    {
        if (sfxDictionary.TryGetValue(sfxName, out AudioClip clip))
        {
            return clip.length;
        }

        Debug.LogWarning("SFX no encontrado: " + sfxName);
        return 0f;
    }

    public void SetMusicVolume(float volume)
    {
        musicMixerGroup.audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        sfxMixerGroup.audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }


    private void InitializeAudiosDictionaries(Dictionary<string, AudioClip> audioDic, Sound[] soundType)
    {
        foreach (var sounds in soundType)
        {
            audioDic[sounds.name] = sounds.clip;
        }
    }
}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
