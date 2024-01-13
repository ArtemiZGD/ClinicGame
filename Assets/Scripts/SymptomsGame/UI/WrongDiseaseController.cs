using TMPro;
using UnityEngine;

public class WrongDiseaseController : MonoBehaviour
{
	[SerializeField] private TMP_Text _wrongText;  // Текстовое поле для отображения информации о неправильной болезни
	[SerializeField] private TMP_Text _rightText;  // Текстовое поле для отображения информации о правильной болезни
	[TextArea]
	[SerializeField] private string _wrongTextExample;  // Пример текста с подсветкой для неправильной болезни
	[TextArea]
	[SerializeField] private string _rightTextExample;  // Пример текста с подсветкой для правильной болезни
	[SerializeField] private char _highlight = '*';  // Символ выделения

	// Установка текстов для неправильной и правильной болезни
	public void SetTexts(DiseaseData wrongDisease, DiseaseData rightDisease)
	{
		_wrongText.text = EditText(_wrongTextExample, wrongDisease);  // Установка текста для неправильной болезни
		_rightText.text = EditText(_rightTextExample, rightDisease);  // Установка текста для правильной болезни
	}

	// Форматирование текста с использованием данных о болезне
	private string EditText(string text, DiseaseData disease)
	{
		string newText = "";

		string[] stringParts = text.Split(_highlight);

		if (stringParts.Length != 2)
		{
			Debug.LogError("Wrong text");
		}

		newText += $"{stringParts[0]}{disease.Name}{stringParts[1]}\n";

		for (int i = 0; i < disease.Symptoms.Length; i++)
		{
			if (i != 0)
			{
				newText += ", ";
			}

			newText += disease.Symptoms[i].Name;
		}

		return newText;
	}
}
