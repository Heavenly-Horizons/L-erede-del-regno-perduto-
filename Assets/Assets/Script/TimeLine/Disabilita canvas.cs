using UnityEngine;

public class DisabilitaCanvas : MonoBehaviour
{
    public static void DisableCanvasBar()
    {
        var HealthBar = GameObject.Find("HealthBar");
        HealthBar.SetActive(false);

        var StaminaBar = GameObject.Find("StaminaBar");
        StaminaBar.SetActive(false);
    }

    public static void EnableCanvasBar()
    {
        var HealthBar = GameObject.Find("HealthBar");
        HealthBar.SetActive(true);

        var StaminaBar = GameObject.Find("StaminaBar");
        StaminaBar.SetActive(true);
    }
}