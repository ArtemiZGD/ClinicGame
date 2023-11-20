using UnityEngine;
using TMPro;

public class AutoResizeBackground : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform; // ������ �� RectTransform ����
    [SerializeField] private RectTransform _backgroundRectTransform; // ������ �� RectTransform ����
    [SerializeField] private TextMeshProUGUI _textMeshPro; // ������ �� TextMeshProUGUI

    private void Start()
    {
        UpdateBackgroundSize();
    }

    public void UpdateBackgroundSize()
    {
        // �������� ������������� ������ ������
        Vector2 preferredTextSize = _textMeshPro.GetPreferredValues(_textMeshPro.text, _backgroundRectTransform.sizeDelta.x, _backgroundRectTransform.sizeDelta.y);

        // ������������� ������ ���� ������ ������� ������
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, preferredTextSize.y);
        _backgroundRectTransform.sizeDelta = new Vector2(_backgroundRectTransform.sizeDelta.x, preferredTextSize.y);
        _backgroundRectTransform.position = new Vector3(_backgroundRectTransform.position.x, _rectTransform.position.y, 0);
    }
}