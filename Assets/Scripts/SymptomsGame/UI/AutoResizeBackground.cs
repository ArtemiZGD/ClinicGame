using UnityEngine;
using TMPro;

public class AutoResizeBackground : MonoBehaviour
{
	[SerializeField] private RectTransform _rectTransform;
	[SerializeField] private RectTransform _backgroundRectTransform;
	[SerializeField] private RectTransform _textAreaRectTransform; 
	[SerializeField] private TMP_Text _textMeshPro;

	private float _horizontalSizeDelta;
	private float _verticalSizeDelta;

	public void UpdateBackgroundSize()
	{
		// ѕолучаем рекомендуемый размер текста
		Vector2 preferredTextSize = _textMeshPro.GetPreferredValues(_textMeshPro.text, _textAreaRectTransform.sizeDelta.x, _textAreaRectTransform.sizeDelta.y);

		// ”станавливаем размер фона равным размеру текста
		_rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, preferredTextSize.y + _verticalSizeDelta);
		_backgroundRectTransform.sizeDelta = new Vector2(_backgroundRectTransform.sizeDelta.x + _horizontalSizeDelta, preferredTextSize.y + _verticalSizeDelta);
		_backgroundRectTransform.position = new Vector3(_backgroundRectTransform.position.x, _rectTransform.position.y, 0);
	}

	private void Start()
	{
		CalculateSizeDelta();
		UpdateBackgroundSize();
	}

	private void CalculateSizeDelta()
	{
		_horizontalSizeDelta = _backgroundRectTransform.sizeDelta.x - _textAreaRectTransform.sizeDelta.x;
		_verticalSizeDelta = _backgroundRectTransform.sizeDelta.y - _textAreaRectTransform.sizeDelta.y;
	}
}