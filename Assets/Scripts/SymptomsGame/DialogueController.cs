using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
	[SerializeField] private ScrollRect _scrollRect;  // ������ ��� ��������� ���������
	[SerializeField] private Transform _contentParent;  // ������������ ������ ��� ����������� ���������
	[SerializeField] private MessageObject _patientMessagePrefab;  // ������ ��������� �� ��������
	[SerializeField] private MessageObject _doctorMessagePrefab;  // ������ ��������� �� �������

	private MessageObject _prefab;  // ������������ ������ ���������

	// ����� ���� ���������
	public void ResetMessages()
	{
		List<Transform> children = new List<Transform>();

		foreach (Transform child in _contentParent)
		{
			children.Add(child);
		}

		for (int i = 0; i < children.Count; i++)
		{
			Destroy(children[i].gameObject);
		}
	}

	// ���������� ������ ���������
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

		StartCoroutine(SetVerticalPosition(0));
	}

	// ��������� ������������ ������� ���������
	private IEnumerator SetVerticalPosition(float position)
	{
		yield return null;

		_scrollRect.verticalNormalizedPosition = position;
	}
}
