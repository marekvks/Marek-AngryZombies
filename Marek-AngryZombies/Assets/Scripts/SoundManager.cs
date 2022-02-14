using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public AudioSource gunSounds;
    public AudioSource zombieSounds;

    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    public void Start()
    {
        LoadOptionsFromJson();
    }

    public void ZombieSound(AudioClip audioClip)
    {
        gunSounds.PlayOneShot(audioClip);
    }
    
    public void GunSound(AudioClip audioClip)
    {
        zombieSounds.PlayOneShot(audioClip);
    }

    public void MasterVol()
    {
        masterVolume = masterSlider.value;
        audioMixer.SetFloat("masterVol", masterVolume);
    }
    public void MusicVol()
    {
        musicVolume = musicSlider.value;
        audioMixer.SetFloat("musicVol", musicVolume);
    }
    
    public void SFXVol()
    {
        sfxVolume = sfxSlider.value;
        audioMixer.SetFloat("sfxVol", sfxVolume);
    }

    private class SaveOptions
    {
        public float masterVolume;
        public float musicVolume;
        public float sfxVolume;
    }

    public void SaveOptionsToJson()
    {
        SaveOptions saveOptions = new SaveOptions();
        saveOptions.masterVolume = masterVolume;
        saveOptions.musicVolume = musicVolume;
        saveOptions.sfxVolume = sfxVolume;

        string json = JsonUtility.ToJson(saveOptions);

        File.WriteAllText(Application.dataPath + "/savedata/options.json", json);
    }

    public void LoadOptionsFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/savedata/options.json");

        SaveOptions saveOptions = JsonUtility.FromJson<SaveOptions>(json);

        masterSlider.value = saveOptions.masterVolume;
        MasterVol();
        musicSlider.value = saveOptions.musicVolume;
        MusicVol();
        sfxSlider.value = saveOptions.sfxVolume;
        SFXVol();
    }
}