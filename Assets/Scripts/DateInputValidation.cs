using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class DateInputValidation : MonoBehaviour
{
	private TMP_InputField _inputField; // Ссылка на компонент TMP_InputField для управления вводом даты.
	private readonly int _maxInputLength = 8; // Максимальная длина ввода даты (ддммгггг).

	private void Start()
	{
		_inputField = GetComponent<TMP_InputField>(); // Получаем ссылку на компонент TMP_InputField.
		_inputField.onValueChanged.AddListener(OnDateValueChanged); // Добавляем слушателя для события изменения значения в поле ввода.
	}

	private void Update()
	{
		_inputField.MoveTextEnd(false); // Перемещение курсора текста в конец поля ввода.
	}

	private void OnDateValueChanged(string newValue)
	{
		// Удаляем из ввода все символы, кроме цифр.
		string cleanedText = Regex.Replace(newValue, "[^0-9]", "");

		if (cleanedText.Length > _maxInputLength)
		{
			cleanedText = cleanedText.Substring(0, _maxInputLength); // Если ввод слишком длинный, обрезаем его до максимальной длины.
		}

		// Добавляем разделители '-' для форматирования даты (дд-мм-гггг).
		if (cleanedText.Length >= 3)
		{
			cleanedText = cleanedText.Insert(2, "-"); // Вставляем '-' после второго символа (дд-).
		}
		if (cleanedText.Length >= 6)
		{
			cleanedText = cleanedText.Insert(5, "-"); // Вставляем '-' после пятого символа (дд-мм-).
		}

		_inputField.text = cleanedText; // Устанавливаем отформатированный текст обратно в поле ввода.
	}
}
