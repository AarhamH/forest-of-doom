using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, effectSounds;
    public AudioSource musicSource, effectSource;
    public Slider slider;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        musicSource.volume = PlayerPrefs.GetFloat("Volume");
        effectSource.volume = PlayerPrefs.GetFloat("Volume");
        if(slider != null) {
            slider.value = PlayerPrefs.GetFloat("Volume");
        }
        PlayMusic("BackgroundMusic");
    }

    private void Update() {
        musicSource.volume = PlayerPrefs.GetFloat("Volume");
        effectSource.volume = PlayerPrefs.GetFloat("Volume");
        if(slider != null) {
            SaveVolume();
        }
    }

    public void PlayMusic(string name) {
        Sound s = Array.Find(musicSounds, x=> x.name == name);
        
        if(s == null) {
            Debug.Log("Sound not found");
        }

        else {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlayEffect(string name) {
        Sound s = Array.Find(effectSounds, x=> x.name == name);

        if(s == null) {
            Debug.Log("Sound not found");
        }

        else {
            effectSource.clip = s.clip;
            effectSource.Play();
        }

    }

    public void StopMusic(string name) {
        Sound s = Array.Find(musicSounds, x=> x.name == name);

        if(s == null) {
            Debug.Log("Sound not found");
        }

        else {
            musicSource.clip = s.clip;
            musicSource.Stop();
        }
    }

    public void SaveVolume() {
        float volumeValue = slider.value;
        slider.value = volumeValue;
        PlayerPrefs.SetFloat("Volume", volumeValue);
        LoadValues();
    }

    private void LoadValues() {
        float volumeValue = PlayerPrefs.GetFloat("Volume");
        slider.value = volumeValue;
        musicSource.volume = volumeValue;
        effectSource.volume = volumeValue;
    }
}
