using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class DateInputValidation : MonoBehaviour
{
	private TMP_InputField _inputField; // ������ �� ��������� TMP_InputField ��� ���������� ������ ����.
	private readonly int _maxInputLength = 8; // ������������ ����� ����� ���� (��������).

	private void Start()
	{
		_inputField = GetComponent<TMP_InputField>(); // �������� ������ �� ��������� TMP_InputField.
		_inputField.onValueChanged.AddListener(OnDateValueChanged); // ��������� ��������� ��� ������� ��������� �������� � ���� �����.
	}

	private void Update()
	{
		_inputField.MoveTextEnd(false); // ����������� ������� ������ � ����� ���� �����.
	}

	private void OnDateValueChanged(string newValue)
	{
		// ������� �� ����� ��� �������, ����� ����.
		string cleanedText = Regex.Replace(newValue, "[^0-9]", "");

		if (cleanedText.Length > _maxInputLength)
		{
			cleanedText = cleanedText.Substring(0, _maxInputLength); // ���� ���� ������� �������, �������� ��� �� ������������ �����.
		}

		// ��������� ����������� '-' ��� �������������� ���� (��-��-����).
		if (cleanedText.Length >= 3)
		{
			cleanedText = cleanedText.Insert(2, "-"); // ��������� '-' ����� ������� ������� (��-).
		}
		if (cleanedText.Length >= 6)
		{
			cleanedText = cleanedText.Insert(5, "-"); // ��������� '-' ����� ������ ������� (��-��-).
		}

		_inputField.text = cleanedText; // ������������� ����������������� ����� ������� � ���� �����.
	}
}
