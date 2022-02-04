using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioSource gunSounds;
    public AudioSource zombieSounds;

    public void ZombieSound(AudioClip audioClip)
    {
        gunSounds.PlayOneShot(audioClip);
    }
    
    public void GunSound(AudioClip audioClip)
    {
        zombieSounds.PlayOneShot(audioClip);
    }
}