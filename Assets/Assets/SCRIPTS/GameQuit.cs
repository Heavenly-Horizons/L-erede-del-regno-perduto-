using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameQuit : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private const string crossfadeExit = "crossfadeExit";
    private const string heavenlyHorizonsQuit = "quitAnim";

    public void QuitGame(bool hasToQuitGame)
    {
        StartCoroutine(QuitAnimation(hasToQuitGame));
    }

    IEnumerator QuitAnimation(bool hasToQuitGame){
        if(hasToQuitGame){
            animator.SetTrigger(heavenlyHorizonsQuit);
            yield return new WaitForSeconds(3);
            Application.Quit();
        } else {
            animator.SetTrigger(crossfadeExit);
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(0);
        }
    }
}
