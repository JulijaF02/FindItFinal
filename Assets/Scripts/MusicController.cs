using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{

    public Slider musicSlider;
    public Slider musicSlider2;
    public AudioSource musicAudioSource;

    private void Start()
    {
        // Load saved music volume (if available) or set a default value
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        SetMusicVolume(savedVolume);
        musicSlider.value = savedVolume;
        musicSlider2.value = savedVolume;
    }

    public void SetMusicVolume(float volume)
    {
        musicAudioSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}
