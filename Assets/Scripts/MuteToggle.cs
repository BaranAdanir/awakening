using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class MuteToggle : MonoBehaviour
{
    Toggle myToggle;

    void Start()
    {
        myToggle = GetComponent<Toggle>();
        if (AudioListener.volume == 0)
        {
            myToggle.isOn = false;
        }
    }
    public void ToggleAudioOnValueChange(bool audioIn)
    {
        if (audioIn == true)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }


}