using UnityEngine;

public class DisabilitaCanvas : MonoBehaviour {
    public static void DisableCanvasBar() {
        GameObject HealthBar = GameObject.Find("HealthBar");
        HealthBar.SetActive(false);

        GameObject StaminaBar = GameObject.Find("StaminaBar");
        StaminaBar.SetActive(false);
    }

    public static void EnableCanvasBar() {
        GameObject HealthBar = GameObject.Find("HealthBar");
        HealthBar.SetActive(true);

        GameObject StaminaBar = GameObject.Find("StaminaBar");
        StaminaBar.SetActive(true);
    }
}