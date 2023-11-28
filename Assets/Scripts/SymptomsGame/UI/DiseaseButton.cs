using TMPro;
using UnityEngine;

public class DiseaseButton : MonoBehaviour
{
	[SerializeField] private TMP_Text _text;

	private DiseaseData _disease;
	private SymptomsGameController _gameController;

	public void Init(SymptomsGameController gameController, DiseaseData disease)
	{
		_gameController = gameController;
		_disease = disease;
		_text.text = disease.Name;
	}

	public void CheckAnswer()
	{
		_gameController.CheckDiseaseAnswer(_disease);
	}
}
