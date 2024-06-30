using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    private const string crossfadeStart = "crossfadeStart";
    private static readonly string PlayerNewGameplay = "PlayerNewGameplay";

    [SerializeField] private Animator crossfade_animation;

    public void LoadGameScene(bool toMainMenu) {
        StartCoroutine(CrossfadeStartOnButtonClick(toMainMenu));
    }

    public void RetryLevel() {
        StartCoroutine(ReplayLevel());
    }

    private IEnumerator ReplayLevel() {
        crossfade_animation.SetTrigger(crossfadeStart);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator CrossfadeStartOnButtonClick(bool toMainMenu) {
        if (toMainMenu) {
            crossfade_animation.SetTrigger(crossfadeStart);
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(0);
        }
        else {
            PlayerPrefs.SetInt(PlayerNewGameplay, 0);
            crossfade_animation.SetTrigger(crossfadeStart);
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}