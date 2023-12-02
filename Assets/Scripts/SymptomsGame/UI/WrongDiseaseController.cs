using TMPro;
using UnityEngine;

public class WrongDiseaseController : MonoBehaviour
{
	[SerializeField] private TMP_Text _wrongText;
	[SerializeField] private TMP_Text _rightText;
	[TextArea]
	[SerializeField] private string _wrongTextExample;
	[TextArea]
	[SerializeField] private string _rightTextExample;
	[SerializeField] private char _highlight = '*';

	public void SetTexts(DiseaseData wrongDisease, DiseaseData rightDisease)
	{
		_wrongText.text = EditText(_wrongTextExample, wrongDisease);
		_rightText.text = EditText(_rightTextExample, rightDisease);
	}

	private string EditText(string text, DiseaseData disease)
	{
		string newText = "";

		string[] stringParts = text.Split(_highlight);

		if (stringParts.Length != 2)
		{
			Debug.LogError("Wrong text");
		}

		newText += stringParts[0] + disease.Name + stringParts[1] + "\n";

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
