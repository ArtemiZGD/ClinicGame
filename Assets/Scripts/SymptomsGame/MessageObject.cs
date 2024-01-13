using TMPro;
using UnityEngine;

public class MessageObject : MonoBehaviour
{
	[SerializeField] private TMP_Text _messageText;  // Текстовое поле для отображения сообщения

	// Устанавливает текст в объект сообщения.
	public void Type(string text)
	{
		_messageText.text = text;  // Устанавливает текст в текстовое поле сообщения
	}
}
