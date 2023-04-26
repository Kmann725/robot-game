using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class InititalizeSound : MonoBehaviour
{
    [SerializeField]
    private AudioMixer am;
    GameObject slider;
    SliderBehavior sb;

    // Start is called before the first frame update
    void Start()
    {
        InitializePlayerPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerPrefs.GetFloat("MasterVolume"));
    }

    public void InitializePlayerPrefs()
    {
        //PlayerPrefs.GetFloat("MasterVolume")
        am.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume")) * 20);
        am.SetFloat("SFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20);
        am.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20);
    }
}
