using System.Collections;
using UnityEngine;

public class DisableHH : MonoBehaviour
{
    [SerializeField] private GameObject heavenlyHorizons;
    [SerializeField] private GameObject gameStartAudio;

    private readonly string hasHeavenlyHorizonsAnimationPlayed = "hasHeavenlyHorizonsAnimationPlayed";

    public void Start(){
        
        if(PlayerPrefs.HasKey(hasHeavenlyHorizonsAnimationPlayed)){
            StartCoroutine(DisableImmediatelyHHAndSound());
        }else{
            PlayerPrefs.SetInt(hasHeavenlyHorizonsAnimationPlayed, 1);
            StartCoroutine(playAndDisable());
        }
    }

    IEnumerator playAndDisable(){
        yield return new WaitForSeconds(6);
        heavenlyHorizons.SetActive(false);
        gameStartAudio.SetActive(false);
    }

    IEnumerator DisableImmediatelyHHAndSound(){
        heavenlyHorizons.SetActive(false);
        gameStartAudio.SetActive(false);
        yield return null;
    }
}
