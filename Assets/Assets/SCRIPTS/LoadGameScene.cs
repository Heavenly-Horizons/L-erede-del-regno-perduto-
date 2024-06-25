using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] private Animator crossfade_animation;
    private const string crossfadeStart = "crossfadeStart";
    private static readonly string PlayerNewGameplay = "PlayerNewGameplay";
    public void LoadGameScene(bool toMainMenu)
    {
        StartCoroutine(CrossfadeStartOnButtonClick(toMainMenu));
    }

    public void ReloadLevel(){
        StartCoroutine(ReloadGameLevel());
    }

    IEnumerator ReloadGameLevel(){
        crossfade_animation.SetTrigger(crossfadeStart);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator CrossfadeStartOnButtonClick(bool toMainMenu)
    {
        if(toMainMenu){
            crossfade_animation.SetTrigger(crossfadeStart);
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(0);
        }else{
            PlayerPrefs.SetInt(PlayerNewGameplay, 0);
            crossfade_animation.SetTrigger(crossfadeStart);
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
