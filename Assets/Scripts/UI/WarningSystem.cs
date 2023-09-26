using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class WarningSystem : MonoBehaviour
{
	public static WarningSystem Instance { get; private set; } // Ссылка на единственный экземпляр класса, доступный через статическое поле.

	[SerializeField] private TMP_Text _warning; // Ссылка на компонент TMP_Text для отображения предупреждений в пользовательском интерфейсе.
	[SerializeField] private float _warningTime; // Время, в течение которого предупреждение будет отображаться.

	private readonly StringBuilder _warningTextBuilder = new(); // Объект StringBuilder для построения текста предупреждений.

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this; // Устанавливаем ссылку на единственный экземпляр этого класса.
		}
		else
		{
			Destroy(gameObject); // Если экземпляр уже существует, уничтожаем этот объект.
		}

		UpdateText(); // Обновляем текст в компоненте TMP_Text.
	}

	public void Warning(string text)
	{
		StartCoroutine(WarningCoroutine(text)); // Вызывает корутину для отображения предупреждения.
	}

	private IEnumerator WarningCoroutine(string text)
	{
		_warningTextBuilder.Insert(0, text); // Добавляем текст предупреждения в начало текущего текста.

		UpdateText(); // Обновляем текст в компоненте TMP_Text.

		yield return new WaitForSeconds(_warningTime); // Ждем некоторое время, прежде чем убрать предупреждение.

		_warningTextBuilder.Remove(_warningTextBuilder.Length - text.Length, text.Length); // Удаляем текст предупреждения из текущего текста.

		UpdateText(); // Обновляем текст в компоненте TMP_Text.
	}

	private void UpdateText()
	{
		_warning.text = _warningTextBuilder.ToString(); // Устанавливаем текст из объекта StringBuilder в компонент TMP_Text для отображения в пользовательском интерфейсе.
	}
}
