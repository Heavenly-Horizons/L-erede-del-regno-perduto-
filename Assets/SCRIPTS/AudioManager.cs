using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BGPref = "BGPref";
    private static readonly string SFXPref = "SFXPref";
    private int firstPlayInt;
    [SerializeField] private Slider BGSlider, SFXSlider;
    [SerializeField] private TextMeshProUGUI BGSliderValue, SFXSliderValue;
    private float BGMusicFloat, SFXFloat;
    [SerializeField] private AudioSource GameStartAudio;
    [SerializeField] private AudioSource BGAudio;
    [SerializeField] private AudioSource[] SFXs;

    private void Awake() {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);
        
        if (firstPlayInt == 0){
            BGMusicFloat = .125f;
            SFXFloat = .5f;
            BGSlider.value = BGMusicFloat;
            SFXSlider.value = SFXFloat;

            PlayerPrefs.SetFloat(BGPref, BGMusicFloat);
            PlayerPrefs.SetFloat(SFXPref, SFXFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }else{
            BGMusicFloat = PlayerPrefs.GetFloat(BGPref);
            SFXFloat = PlayerPrefs.GetFloat(SFXPref);

            BGSlider.value = BGMusicFloat;
            SFXSlider.value = SFXFloat;

            BGSliderValue.text = (BGSlider.value + 80f).ToString("0");
            SFXSliderValue.text = (SFXSlider.value + 80f).ToString("0");
        }
    }

    public void SaveSoundSettings(){
        PlayerPrefs.SetFloat(BGPref, BGSlider.value);
        PlayerPrefs.SetFloat(SFXPref, SFXSlider.value);
    }

    private void OnApplicationFocus(bool focusStatus) {
        if(!focusStatus){
            SaveSoundSettings();
        }
    }

    public void UpdateSound(){
        BGAudio.volume = (BGSlider.value + 80f) / 100;
        GameStartAudio.volume = (SFXSlider.value + 80f) / 100;

        foreach (AudioSource sfx in SFXs){
            sfx.volume = (SFXSlider.value + 80f) / 100;
        }
        
        BGSliderValue.text = (BGSlider.value + 80f).ToString("0");
        SFXSliderValue.text = (SFXSlider.value + 80f).ToString("0");
    }
}
