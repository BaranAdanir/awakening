using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    public Slider volumeSlider;

    private float musicVolume = 1f;
    void Start()
    {
        audioSource.Play();
        musicVolume = PlayerPrefs.GetFloat("volume");
        audioSource.volume = musicVolume;
        volumeSlider.value = musicVolume;
    }


    void Update()
    {
        audioSource.volume = musicVolume;
        PlayerPrefs.SetFloat("volume", musicVolume);
    }

    public void VolumeUpdater(float volume)
    {
        musicVolume = volume;
    }
}
