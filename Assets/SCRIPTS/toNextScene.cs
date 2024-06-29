using Unity.VisualScripting;
using UnityEngine;

public class toNextScene : MonoBehaviour {
    [SerializeField] private GameObject nextSceneLoader;
    [SerializeField] private GameObject nextLevelTrigger;
    [SerializeField] private GameObject SoundSaver;
    [SerializeField] private GameObject PlayerSaver;
    private PlayerStats playersaver;
    private SceneLoader sceneLoader;
    private AudioManager soundsaver;

    private void Start() {
        //next level trigger
        var nextLevelTriggerBoxCollider2D = nextLevelTrigger.GetComponent<BoxCollider2D>();
        Debug.Log(nextLevelTriggerBoxCollider2D != null
            ? "nextLevelTrigger.GetComponent<BoxCollider2D>() in toNextScene istanziato"
            : "nextLevelTrigger.GetComponent<BoxCollider2D>() in toNextScene non istanziato");
        nextLevelTriggerBoxCollider2D.isTrigger = false;

        //scene loader
        sceneLoader = nextSceneLoader.GetComponent<SceneLoader>();
        Debug.Log(sceneLoader != null
            ? "nextSceneLoader.GetComponent<SceneLoader>() in toNextScene istanziato"
            : "nextSceneLoader.GetComponent<SceneLoader>() in toNextScene non istanziato");
        //audioManger
        soundsaver = SoundSaver.GetComponent<AudioManager>();
        Debug.Log(sceneLoader != null
            ? "SoundSaver.GetComponent<AudioManager>() in toNextScene istanziato"
            : "SoundSaver.GetComponent<AudioManager>() in toNextScene non istanziato");
        //player stats
        playersaver = PlayerSaver.GetComponent<PlayerStats>();
        Debug.Log("Scene Loader component is null: " + sceneLoader.IsUnityNull());
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            //next level trigger
            var nextLevelTriggerBoxCollider2D = nextLevelTrigger.GetComponent<BoxCollider2D>();
            Debug.Log(nextLevelTriggerBoxCollider2D != null
                ? "nextLevelTrigger.GetComponent<BoxCollider2D>() in toNextScene istanziato"
                : "nextLevelTrigger.GetComponent<BoxCollider2D>() in toNextScene non istanziato");
            nextLevelTriggerBoxCollider2D.isTrigger = false;

            Debug.Log(nextLevelTrigger.GetComponent<BoxCollider2D>().isTrigger);
            soundsaver.SaveSoundSettings();
            playersaver.SavePlayerAndScene();
            sceneLoader.LoadGameScene(false);
        }
    }
}