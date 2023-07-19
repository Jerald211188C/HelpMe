using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolumeManager : MonoBehaviour
{

    public AudioMixer mixer;
    public Slider masterSlider;

    // Start is called before the first frame update
    void Start()
    {
        masterSlider.value = PlayerPrefsManager.Load("MasterVolume");
        Debug.Log(masterSlider.value);
        if (masterSlider.value == 0)
        {
            masterSlider.value = -20;
        }
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMasterVolume()
    {
        mixer.SetFloat("MasterVolume", masterSlider.value);
        PlayerPrefsManager.Save("MasterVolume", masterSlider.value);
    }
}
