using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderBehavior : MonoBehaviour
{
    [SerializeField] private AudioMixer am;
    public string sliderName;
    GameObject soundInit;
    InititalizeSound init;
    Slider slider;

    float sliderVal;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();

        slider.value = PlayerPrefs.GetFloat(GetSliderName());

        SetVolume(slider.value);

        //init.InitializePlayerPrefs(GetSliderName());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetSliderName()
    {
        return sliderName;
    }

    public void SetVolume(float sliderValue)
    {
        sliderVal = sliderValue;

        am.SetFloat(GetSliderName(), Mathf.Log10(sliderValue) * 20);

        PlayerPrefs.SetFloat(GetSliderName(), sliderVal);
    }
}
