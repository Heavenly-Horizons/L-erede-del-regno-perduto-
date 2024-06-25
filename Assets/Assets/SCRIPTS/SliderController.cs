using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VolumeValueShow : MonoBehaviour {
    [SerializeField] private Slider slider = null;
    [SerializeField] private TextMeshProUGUI sliderText = null;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = slider.maxValue;
        sliderText.text = (slider.value + 80).ToString("0");
    }

    public void sliderChange(float volume)
    {
        float localVolume = volume + 80;
        sliderText.text = localVolume.ToString("0");
    }

}
