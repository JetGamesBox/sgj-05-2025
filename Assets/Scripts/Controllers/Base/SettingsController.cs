using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsController
{
    public SettingsController ()
    {
        Application.targetFrameRate = 60;
        //QualitySettings.vSyncCount = 60;
    }


    public void ApplyAudioSettings(AudioMixer audioMixer)
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        float effectsVolume = PlayerPrefs.GetFloat("EffectsVolume");



    }

}
