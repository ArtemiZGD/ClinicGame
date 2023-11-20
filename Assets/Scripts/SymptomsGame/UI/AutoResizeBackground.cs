using UnityEngine;
using TMPro;

public class AutoResizeBackground : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform; // Ссылка на RectTransform фона
    [SerializeField] private RectTransform _backgroundRectTransform; // Ссылка на RectTransform фона
    [SerializeField] private TextMeshProUGUI _textMeshPro; // Ссылка на TextMeshProUGUI

    private void Start()
    {
        UpdateBackgroundSize();
    }

    public void UpdateBackgroundSize()
    {
        // Получаем рекомендуемый размер текста
        Vector2 preferredTextSize = _textMeshPro.GetPreferredValues(_textMeshPro.text, _backgroundRectTransform.sizeDelta.x, _backgroundRectTransform.sizeDelta.y);

        // Устанавливаем размер фона равным размеру текста
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, preferredTextSize.y);
        _backgroundRectTransform.sizeDelta = new Vector2(_backgroundRectTransform.sizeDelta.x, preferredTextSize.y);
        _backgroundRectTransform.position = new Vector3(_backgroundRectTransform.position.x, _rectTransform.position.y, 0);
    }
}