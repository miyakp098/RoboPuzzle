using UnityEngine;
using TMPro; // TextMeshProを使用するためのnamespace
using UnityEngine.EventSystems;

public class ButtonTextColorTMP : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text buttonText;
    
    private Color normalColor = new Color(150f / 255f, 150f / 255f, 150f / 255f, 1f);
    private Color hoverColor = Color.black;

    private void Start()
    {
        if (buttonText == null)
        {
            buttonText = GetComponentInChildren<TMP_Text>();
        }
        SetNormalColor();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetHoverColor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetNormalColor();
    }

    private void SetHoverColor()
    {
        buttonText.color = hoverColor;
    }

    private void SetNormalColor()
    {
        buttonText.color = normalColor;
    }
}
