using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, effectSounds;
    public AudioSource musicSource, effectSource;


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
        PlayMusic("BackgroundMusic");
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
            musicSource.Play();
        }

    }
}
