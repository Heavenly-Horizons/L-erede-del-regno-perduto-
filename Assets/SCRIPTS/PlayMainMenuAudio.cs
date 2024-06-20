using System.Collections;
using UnityEngine;

public class PlayMainMenuAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioToWait;
    [SerializeField] private AudioSource[] audiosToPlay; 
    private void Start()
    {
        StartCoroutine(PlayMainMenuAudioClips());
    }

    IEnumerator PlayMainMenuAudioClips(){
        yield return new WaitForSeconds(audioToWait.clip.length);
        foreach(AudioSource audio in audiosToPlay){
            audio.Play();
        }
    }
}
