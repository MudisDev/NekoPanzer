using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager sharedInstance;

    [SerializeField] AudioClip shootAudio;
    [SerializeField] AudioClip initialMusic;
    [SerializeField] AudioClip gameplayMusic1;

    [SerializeField] AudioSource audioSourceMusic;
    [SerializeField] AudioSource audioSourceSfx;

    [SerializeField] Slider sliderMusicVol;
    [SerializeField] Slider sliderSFXVol;
    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
            return;
        }
        sharedInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        /* this.audioSourceMusic.volume = 0.30f;
        this.audioSourceSfx.volume = 1; */
        LoadVolumeSettings();
    }

    private void Update()
    {
       
    }

    public void PlayShoot()
    {
        this.audioSourceSfx.PlayOneShot(this.shootAudio);
    }

    public void SetMusicVolume(float vol)
    {
        this.audioSourceMusic.volume = vol;
        PlayerPrefs.SetFloat("MusicVolume", vol);
        PlayerPrefs.Save();
    }

    public void SetSfxVolume(float vol)
    {
        this.audioSourceSfx.volume = vol;
        PlayerPrefs.SetFloat("SfxVolume", vol);
        PlayerPrefs.Save();
    }

    public void SetTrackMusic(GameState gameState)
    {
        AudioClip newClip = null;
        switch (gameState)
        {
            case GameState.menu:
                newClip = this.initialMusic;
                break;
            case GameState.inGame:
                newClip = this.gameplayMusic1;
                break;
        }

        if (this.audioSourceMusic.clip != newClip)
        {
            this.audioSourceMusic.clip = newClip;
            this.audioSourceMusic.loop = true;
            this.audioSourceMusic.Play();
        }

    }

    private void LoadVolumeSettings()
    {
        Debug.Log("entro a Load VOlumen Setting");
        //PlayerPrefs.DeleteKey("MusicVolume");
        //PlayerPrefs.DeleteKey("SfxVolume");
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            Debug.Log("TIene llave musicvolume");

            float musicVolume = PlayerPrefs.GetFloat("MusicVolume");

            this.sliderMusicVol.value = musicVolume;
            this.audioSourceMusic.volume = musicVolume;
        }
        else
        {
            PlayerPrefs.SetFloat("MusicVolume", 0.3f);
            this.sliderMusicVol.value = 0.3f;
            this.audioSourceMusic.volume = 0.3f;
        }

        if (PlayerPrefs.HasKey("SfxVolume"))
        {

            Debug.Log("TIene llave sfxVolume");

            float sfxVolume = PlayerPrefs.GetFloat("SfxVolume");

            this.sliderSFXVol.value = sfxVolume;
            this.audioSourceSfx.volume = sfxVolume;
        }
        else
        {
            PlayerPrefs.SetFloat("SfxVolume", 0.8f);
            this.sliderSFXVol.value = 0.8f;
            this.audioSourceSfx.volume = 0.8f;
        }
    }
}
