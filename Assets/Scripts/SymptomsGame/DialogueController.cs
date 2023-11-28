using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
	[SerializeField] private ScrollRect _scrollRect;
	[SerializeField] private Transform _contentParent;
	[SerializeField] private MessageObject _patientMessagePrefab;
	[SerializeField] private MessageObject _doctorMessagePrefab;

	private MessageObject _prefab;
	
	public void ResetMessages()
	{
		List<Transform> childs = new();

		foreach (Transform child in _contentParent)
		{
			childs.Add(child);
		}

		for (int i = 0; i < childs.Count; i++)
		{
			Destroy(childs[i].gameObject);
		}
	}

	public void AddMessage(Message message)
	{
		if (message.Sender == Sender.Doctor)
		{
			_prefab = _doctorMessagePrefab;
		}
		else if (message.Sender == Sender.Patient)
		{
			_prefab = _patientMessagePrefab;
		}

		MessageObject newMessage = Instantiate(_prefab, _contentParent);
		newMessage.Type(message.Text);

		StartCoroutine(SetVerticalPositon(0));
	}

	private IEnumerator SetVerticalPositon(float position)
	{
		yield return null;

		_scrollRect.verticalNormalizedPosition = position;
	}
}
