using TMPro;
using UnityEngine;

public class WrongMedicationController : MonoBehaviour
{
	[SerializeField] private TMP_Text _wrongText;
	[SerializeField] private TMP_Text _rightText;
	[TextArea]
	[SerializeField] private string _wrongTextExample;
	[TextArea]
	[SerializeField] private string _rightTextExample;
	[SerializeField] private char _highlight = '*';

	public void SetTexts(DiseaseData rightDisease)
	{
		_wrongText.text = EditTextWrongMed(_wrongTextExample, rightDisease);
		_rightText.text = EditText(_rightTextExample, rightDisease);
	}

	private string EditTextWrongMed(string text, DiseaseData rightDisease)
	{
		string newText = "";

		string[] stringParts = text.Split(_highlight);

		if (stringParts.Length != 2)
		{
			Debug.LogError("Wrong text");
		}

		newText += stringParts[0] + rightDisease.Name + stringParts[1] + "\n";

		return newText;
	}

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
