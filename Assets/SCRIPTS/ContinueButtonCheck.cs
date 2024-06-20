using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueButtonCheck : MonoBehaviour
{
    private static readonly string PlayerNewGameplay = "PlayerNewGameplay";
    private static readonly string PlayerCurrentScene = "PlayerCurrentScene";
    private int PlayerNewGameplayInt;
    private EventTrigger eventTrigger;
    private Button button;
    private TextMeshProUGUI textInside;

    [SerializeField] private Animator crossfade_animation;
    private const string crossfadeStart = "crossfadeStart";
    void Start()
    {
        PlayerNewGameplayInt = PlayerPrefs.GetInt(PlayerNewGameplay);
        Debug.Log("Continue button script, player new gameplay int: " + PlayerNewGameplayInt);
        button = gameObject.GetComponent<Button>();
        textInside = gameObject.GetComponentInChildren<TextMeshProUGUI>();

        if (!PlayerPrefs.HasKey(PlayerNewGameplay))
        {
            eventTrigger = gameObject.GetComponent<EventTrigger>(); 
            button.interactable = false;
            textInside.color = new Color(.42f, .42f, .42f, 1f);
            Debug.Log(textInside.color);
            Destroy(eventTrigger);
        }
    }

    public void ContinueButtonLoadScene(){
        int sceneToLoad = PlayerPrefs.GetInt(PlayerCurrentScene);
        StartCoroutine(LoadGameSceneWithIndex(sceneToLoad));
    }

    IEnumerator LoadGameSceneWithIndex(int sceneID){
        crossfade_animation.SetTrigger(crossfadeStart);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneID);
    }
}
