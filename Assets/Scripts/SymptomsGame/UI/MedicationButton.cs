using TMPro;
using UnityEngine;

public class MedicationButton : MonoBehaviour
{
	[SerializeField] private TMP_Text _text;

	private MedicationData _medication;
	private SymptomsGameController _gameController;

	public void Init(SymptomsGameController gameController, MedicationData medication)
	{
		_gameController = gameController;
		_medication = medication;
		_text.text = medication.Name;
	}

	public void CheckAnswer()
	{
		_gameController.CheckMedicationAnswer(_medication);
	}
}
