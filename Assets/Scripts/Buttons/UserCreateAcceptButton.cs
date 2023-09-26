using System;
using TMPro;
using UnityEngine;

public class UserCreateAcceptButton : MonoBehaviour
{
	[SerializeField] private UIManager _manager;
	[SerializeField] private WarningSystem _warningSystem;
	[SerializeField] private TMP_InputField _nameInputField;
	[SerializeField] private TMP_InputField _dateInputField;
	[SerializeField] private UserType _userType;
	[SerializeField] private string _wrongNameWarningText = "����������� ������� ���\n";
	[SerializeField] private string _wrongUserTypeWarningText = "�������� ��� ������������\n"; 
	[SerializeField] private string _wrongDateWarningText = "������������ ���� ��������\n";
	[SerializeField] private int _minWordsCountInName = 2;

	private bool _isInputCorrect;

	public void TryToAccept()
	{
		// ������ ���� �� �������� ������������ �����
		_isInputCorrect = true;

		// ���� ��� � ���� ������� ������� ������ �������������� ������ ������
		if (IsNameValid() == false)
		{
			_warningSystem.Warning(_wrongNameWarningText);
			_isInputCorrect = false;
		}
		if (IsDateValid() == false)
		{
			_warningSystem.Warning(_wrongDateWarningText);
			_isInputCorrect = false;
		}

		// ���� ��� ������� �����, ��������� �������������� ����
		if (_isInputCorrect)
		{
			OpenMenu();
		}
	}

	private bool IsNameValid()
	{
		// ����������� ���� (������)
		char[] delimiters = { ' ' };

		// �������� ��������� ����� �� ��������� �����
		string[] words = _nameInputField.text.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries);

		// ��������, ��� ���������� ���� ������ ������������ ����������
		return words.Length >= _minWordsCountInName;
	}

	private bool IsDateValid()
	{
		// ��������� ������������� ��������� ������ � ����
		if (DateTime.TryParse(_dateInputField.text, out _))
		{
			// ���� ���� ������ ��������, ������� �� ����������
			return true;
		}

		// ���� �� ������� ������������� ��������� ������ � ����, ������� ���� ������������
		return false;
	}

	private void OpenMenu()
	{
		// � ����������� �� ���� ������������ ��������� �������������� ����
		if (_userType == UserType.Patient)
		{
			_manager.OpenPatientMenu();
		}
		else if (_userType == UserType.Doctor)
		{
			_manager.OpenDoctorMenu();
		}
		else
		{
			_warningSystem.Warning(_wrongUserTypeWarningText);
		}
	}
}
