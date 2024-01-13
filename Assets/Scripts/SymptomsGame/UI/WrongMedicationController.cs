using TMPro;
using UnityEngine;

public class WrongMedicationController : MonoBehaviour
{
	[SerializeField] private TMP_Text _wrongText;  // “екстовое поле дл€ отображени€ информации о неправильных лекарствах
	[SerializeField] private TMP_Text _rightText;  // “екстовое поле дл€ отображени€ информации о правильных лекарствах
	[TextArea]
	[SerializeField] private string _wrongTextExample;  // ѕример текста с подсветкой дл€ неправильных лекарств
	[TextArea]
	[SerializeField] private string _rightTextExample;  // ѕример текста дл€ правильных лекарств
	[SerializeField] private char _highlight = '*';  // —имвол выделени€

	// ”становка текстов дл€ неправильных и правильных лекарств
	public void SetTexts(DiseaseData rightDisease)
	{
		_wrongText.text = EditTextWrongMed(_wrongTextExample, rightDisease);  // ”становка текста дл€ неправильных лекарств
		_rightText.text = EditText(_rightTextExample, rightDisease);  // ”становка текста дл€ правильных лекарств
	}

	// ‘орматирование текста с использованием данных о правильной болезни
	private string EditTextWrongMed(string text, DiseaseData rightDisease)
	{
		string newText = "";

		string[] stringParts = text.Split(_highlight);

		if (stringParts.Length != 2)
		{
			Debug.LogError("Wrong text");
		}

		newText += $"{stringParts[0]}{rightDisease.Name}{stringParts[1]}\n";

		return newText;
	}

	// ‘орматирование текста с использованием данных о правильной болезни
	private string EditText(string text, DiseaseData rightDisease)
	{
		string newText = text;

		for (int i = 0; i < rightDisease.Medications.Length; i++)
		{
			if (i != 0)
			{
				newText += ", ";
			}

			newText += rightDisease.Medications[i].Name;
		}

		return newText;
	}
}
