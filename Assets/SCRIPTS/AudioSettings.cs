using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AudioSettings : MonoBehaviour
{
     private static readonly string BGPref = "BGPref";
     private static readonly string SFXPref = "SFXPref";
     [SerializeField] private Slider BGSlider, SFXSlider;
     [SerializeField] private TextMeshProUGUI BGSliderValue, SFXSliderValue;
     private float BGMusicFloat, SFXFloat;
     [SerializeField] private AudioSource BGAudio;
     [SerializeField] private AudioSource[] SFXs;

     void Awake() {
          if(SceneManager.GetActiveScene().buildIndex != 0){ BGAudio.Play(); }
          ContinueSettings();
     }

     public void ContinueSettings(){
          BGMusicFloat = PlayerPrefs.GetFloat(BGPref);
          SFXFloat = PlayerPrefs.GetFloat(SFXPref);

          Debug.Log(BGMusicFloat);
          Debug.Log(SFXFloat);

          BGSlider.value = BGMusicFloat;
          SFXSlider.value = SFXFloat;

          BGSliderValue.text = (BGMusicFloat + 80f).ToString("#0");
          SFXSliderValue.text = (SFXFloat + 80f).ToString("#0");

          BGAudio.volume = (BGMusicFloat + 80f ) / 100;
          foreach(AudioSource audio in SFXs){
               audio.volume = (SFXFloat + 80f) / 100;
          }
     }
}
