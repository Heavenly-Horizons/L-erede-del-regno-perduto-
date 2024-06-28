using UnityEngine;

public class stopEverything : MonoBehaviour {
    public void stopGame() {
        Time.timeScale = 0;
    }

    public void resumeGame() {
        Time.timeScale = 1;
    }

    public static void stop() {
        Time.timeScale = 0;
    }

    public static void resume() {
        Time.timeScale = 1;
    }
}