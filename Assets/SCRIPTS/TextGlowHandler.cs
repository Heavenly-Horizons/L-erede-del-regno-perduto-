using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextHoverGlow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public TMP_FontAsset glowFontAsset;
    private TMP_FontAsset originalFontAsset;
    private TextMeshProUGUI textMeshPro;

    private void Start() {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        Debug.Log(textMeshPro != null
            ? "GetComponent<TextMeshProUGUI>() in TextGlowHandler istanziato"
            : "GetComponent<TextMeshProUGUI>() in TextGlowHandler non istanziato");
        originalFontAsset = textMeshPro.font;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        textMeshPro.color = new(1, 1, 1, 1);
        textMeshPro.font = glowFontAsset;
    }

    public void OnPointerExit(PointerEventData eventData) {
        textMeshPro.color = new(0, 0, 0, 1);
        textMeshPro.font = originalFontAsset;
    }
}