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
	[SerializeField] private string _wrongNameWarningText = "Неправильно введено ФИО\n";
	[SerializeField] private string _wrongUserTypeWarningText = "Неверный тип пользователя\n"; 
	[SerializeField] private string _wrongDateWarningText = "Неправильная дата рождения\n";
	[SerializeField] private int _minWordsCountInName = 2;

	private bool _isInputCorrect;

	public void TryToAccept()
	{
		// Вводим флаг на проверку корректности ввода
		_isInputCorrect = true;

		// Если ФИО и дата введены неверно выводи соответсвующую ошибку ошибку
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

		// Если все введено верно, открываем соответсвующее меню
		if (_isInputCorrect)
		{
			OpenMenu();
		}
	}

	private bool IsNameValid()
	{
		// Разделители слов (пробел)
		char[] delimiters = { ' ' };

		// Разделим введенный текст на отдельные слова
		string[] words = _nameInputField.text.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries);

		// Проверим, что количество слов больше минимального количества
		return words.Length >= _minWordsCountInName;
	}

	private bool IsDateValid()
	{
		// Попробуем преобразовать введенную строку в дату
		if (DateTime.TryParse(_dateInputField.text, out _))
		{
			// Если дата прошла проверку, считаем ее корректной
			return true;
		}

		// Если не удалось преобразовать введенную строку в дату, считаем дату некорректной
		return false;
	}

	private void OpenMenu()
	{
		// В зависимости от типа пользователя открываем соответсвующее меню
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
