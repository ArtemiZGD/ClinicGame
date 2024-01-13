using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _dialogText;
	[SerializeField] private ScrollRect _scrollRect;
	[SerializeField] private Scrollbar _scrollbar;

	private List<string> _messages = new();

	private void Start()
	{
		for (int i = 0; i < 20; i++)
		{
			AddMessage("1");
		}
	}

	public void AddMessage(string message)
	{
		_messages.Add(message);
		UpdateDialogText();
		LimitScrollbarPosition();
	}

	private void UpdateDialogText()
	{
		_dialogText.text = string.Join("\n", _messages);

		Canvas.ForceUpdateCanvases(); // ќбновление канваса дл€ правильного расчета размеров текста

		_scrollRect.verticalNormalizedPosition = 0f; // ѕрокрутите вниз при добавлении нового сообщени€
	}

	private void LimitScrollbarPosition()
	{
		float contentHeight = _scrollRect.content.rect.height;
		float viewportHeight = _scrollRect.viewport.rect.height;

		float maxNormalizedPosition = Mathf.Max(0, 1 - viewportHeight / contentHeight);

		_scrollbar.value = Mathf.Clamp(_scrollbar.value, 0, maxNormalizedPosition);
	}
}
