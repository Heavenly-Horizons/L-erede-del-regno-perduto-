using Unity.VisualScripting;
using UnityEngine;

public class toNextScene : MonoBehaviour
{
    [SerializeField] private GameObject nextSceneLoader;
    [SerializeField] private GameObject nextLevelTrigger;
    [SerializeField] private GameObject SoundSaver;
    [SerializeField] private GameObject PlayerSaver;
    private AudioManager soundsaver;
    private PlayerStats playersaver;
    private SceneLoader sceneLoader;

    private void Awake()
    {
        nextLevelTrigger.GetComponent<BoxCollider2D>().isTrigger = false;
        Debug.Log(nextLevelTrigger.GetComponent<BoxCollider2D>().isTrigger);
        sceneLoader = nextSceneLoader.GetComponent<SceneLoader>();
        soundsaver = SoundSaver.GetComponent<AudioManager>();
        playersaver = PlayerSaver.GetComponent<PlayerStats>();
        Debug.Log("Scene Loader component is null: " + sceneLoader.IsUnityNull());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nextLevelTrigger.GetComponent<BoxCollider2D>().isTrigger = true;
            Debug.Log(nextLevelTrigger.GetComponent<BoxCollider2D>().isTrigger);
            soundsaver.SaveSoundSettings();
            playersaver.SavePlayerAndScene();
            sceneLoader.LoadGameScene(false);
        }
    }
}
