using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        } else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    public void Save()
    {
        //float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value); 
        //Load();
    }

    public void Load()
    {
        float volumeValue = PlayerPrefs.GetFloat("musicVolume");
        volumeSlider.value = volumeValue;
        //AudioListener.volume = volumeValue;
    }
}