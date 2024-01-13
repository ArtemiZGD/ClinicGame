using TMPro;
using UnityEngine;

public class GreetingButton : MonoBehaviour
{
	[SerializeField] private DialogueTextData _dialogueTextData;  // Данные текста приветствия
	[SerializeField] private TMP_Text _text;  // Текстовое поле для отображения приветствия

	// Вызывается при старте объекта
	private void Start()
	{
		_text.text = _dialogueTextData.DoctorGreeting;  // Устанавливаем текст приветствия из данных в текстовое поле
	}
}
