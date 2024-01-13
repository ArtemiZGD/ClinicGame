using UnityEngine;
using TMPro;

public class AutoResizeBackground : MonoBehaviour
{
	[SerializeField] private RectTransform _rectTransform;  // Размеры общего блока
	[SerializeField] private RectTransform _backgroundRectTransform;  // Размеры фона
	[SerializeField] private RectTransform _textAreaRectTransform;  // Размеры области текста
	[SerializeField] private TMP_Text _textMeshPro;  // Компонент текста

	private float _horizontalSizeDelta;  // Горизонтальная разница между размерами фона и области текста
	private float _verticalSizeDelta;    // Вертикальная разница между размерами фона и области текста

	// Обновление размеров фона в соответствии с размером текста
	public void UpdateBackgroundSize()
	{
		// Получаем рекомендуемый размер текста
		Vector2 preferredTextSize = _textMeshPro.GetPreferredValues(_textMeshPro.text, _textAreaRectTransform.sizeDelta.x, _textAreaRectTransform.sizeDelta.y);

		// Устанавливаем размер блока и фона равным размеру текста
		_rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, preferredTextSize.y + _verticalSizeDelta);
		_backgroundRectTransform.sizeDelta = new Vector2(_backgroundRectTransform.sizeDelta.x + _horizontalSizeDelta, preferredTextSize.y + _verticalSizeDelta);

		// Позиционируем фон по вертикали относительно блока
		_backgroundRectTransform.position = new Vector3(_backgroundRectTransform.position.x, _rectTransform.position.y, 0);
	}

	// Вычисление разницы в размерах между фоном и областью текста
	private void CalculateSizeDelta()
	{
		_horizontalSizeDelta = _backgroundRectTransform.sizeDelta.x - _textAreaRectTransform.sizeDelta.x;
		_verticalSizeDelta = _backgroundRectTransform.sizeDelta.y - _textAreaRectTransform.sizeDelta.y;
	}

	// Вызывается при старте объекта
	private void Start()
	{
		CalculateSizeDelta();  // Вычисляем разницу в размерах
		UpdateBackgroundSize();  // Обновляем размеры фона
	}
}
