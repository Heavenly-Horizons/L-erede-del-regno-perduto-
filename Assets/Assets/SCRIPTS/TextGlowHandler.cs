using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextHoverGlow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI textMeshPro;
    private TMP_FontAsset originalFontAsset;
    public TMP_FontAsset glowFontAsset;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        originalFontAsset = textMeshPro.font;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textMeshPro.color = new Color(1,1,1,1);
        textMeshPro.font = glowFontAsset;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMeshPro.color = new Color(0, 0, 0, 1);
        textMeshPro.font = originalFontAsset;
    }
}
