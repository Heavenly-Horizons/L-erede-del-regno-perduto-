using Unity.VisualScripting;
using UnityEngine;

public class toNextScene : MonoBehaviour {
    [SerializeField] private SceneLoader nextSceneLoader;
    [SerializeField] private BoxCollider2D nextLevelTrigger;
    [SerializeField] private AudioManager SoundSaver;
    [SerializeField] private PlayerStats PlayerSaver;

    private void Awake() {
        nextLevelTrigger.isTrigger = false;
        Debug.Log(nextLevelTrigger.isTrigger);
        Debug.Log("Scene Loader component is null: " + nextSceneLoader.IsUnityNull());
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            nextLevelTrigger.isTrigger = true;
            Debug.Log(nextLevelTrigger.isTrigger);
            SoundSaver.SaveSoundSettings();
            PlayerSaver.SavePlayerAndScene();
            nextSceneLoader.LoadGameScene(false);
        }
    }
}