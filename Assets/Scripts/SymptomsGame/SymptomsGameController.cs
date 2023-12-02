using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PatientDisplay)), RequireComponent(typeof(DialogueController)), RequireComponent(typeof(PatientGenerator))]
public class SymptomsGameController : MonoBehaviour
{
	[SerializeField] private HeartsController _heartsController;

	[Header("UI")]
	[SerializeField] private ScreenSwitcher _screenSwitcher;
	[SerializeField] private GameObject _gameOverMenu;
	[SerializeField] private TMP_Text _symptomsText;
	[SerializeField] private string _symptomsTextBeginning;
	[SerializeField] private char _symptomsTextSeparator = '-';
	[SerializeField] private TMP_Text _diagnosisText;
	[SerializeField] private TMP_Text _medicationText;

	[Header("Buttons")]
	[SerializeField] private List<DiseaseButton> _diseaseButtons;
	[SerializeField] private WrongDiseaseController _reloadDiagnosisButton;
	[SerializeField] private WrongMedicationController _reloadMedicationButton;
	[SerializeField] private List<MedicationButton> _medicationButtons;

	public int DiseaseButtonsCount => _diseaseButtons.Count;
	public int MedicationButtonsCount => _medicationButtons.Count;
	public int Score => _score;

	private PatientGenerator _patientGenerator;
	private DialogueController _dialogueController;
	private PatientDisplay _patientDisplay;

	private Patient _patient;

	private int _score;

	public void CheckDiseaseAnswer(DiseaseData disease)
	{
		if (disease.Name == _patient.Disease.Name)
		{
			_diagnosisText.text = disease.Name;
			SetActiveButtons(ButtonType.Medication);
		}
		else
		{
			TakeDamage(ButtonType.ReloadDiagnosis);
			_reloadDiagnosisButton.SetTexts(disease, _patient.Disease);
		}
	}

	public void CheckMedicationAnswer(MedicationData medication)
	{
		if (_patientGenerator.Patient.IsMedicationFit(medication))
		{
			_medicationText.text = medication.Name;
			_score++;
			GenerateNewPatient();
		}
		else
		{
			TakeDamage(ButtonType.ReloadMedication);
			_reloadMedicationButton.SetTexts(medication, _patient.Disease);
		}
	}

	public void GenerateNewPatient()
	{
		_patient = _patientGenerator.GeneratePatient();
		_patientDisplay.DisplayPatient(_patient);
		_dialogueController.ResetMessages();
		SetActiveButtons(ButtonType.Disease);
		DisplaySymptoms();
		ClearTexts();
	}

	public void InitDisease(int index, DiseaseData disease)
	{
		_diseaseButtons[index].Init(this, disease);
	}

	public void InitMedication(int index, MedicationData medication)
	{
		_medicationButtons[index].Init(this, medication);
	}

	private void Awake()
	{
		_patientDisplay = GetComponent<PatientDisplay>();
		_dialogueController = GetComponent<DialogueController>();
		_patientGenerator = GetComponent<PatientGenerator>();
	}

	private void Start()
	{
		_score = 0;
		GenerateNewPatient();
	}

	private void DisplaySymptoms()
	{
		_symptomsText.text = "";
		_symptomsText.text += _symptomsTextBeginning;

		foreach (string symptom in _patient.SelectedSymptoms)
		{
			_symptomsText.text += "\n";
			_symptomsText.text += _symptomsTextSeparator;
			_symptomsText.text += symptom;
		}
	}

	public void ClearTexts()
	{
		_diagnosisText.text = "";
		_medicationText.text = "";
	}

	private void TakeDamage(ButtonType buttonType)
	{
		_heartsController.TakeDamage();

		if (_heartsController.IsAlive == false)
		{
			_heartsController.ResetHearts();
			_screenSwitcher.SwitchScreen(_gameOverMenu);
			_score = 0;
		}
		else
		{
			SetActiveButtons(buttonType);
		}
	}

	private void SetActiveButtons(ButtonType buttonType)
	{
		foreach (DiseaseButton button in _diseaseButtons)
		{
			button.gameObject.SetActive(buttonType == ButtonType.Disease);
		}

		foreach (MedicationButton button in _medicationButtons)
		{
			button.gameObject.SetActive(buttonType == ButtonType.Medication);
		}

		_reloadDiagnosisButton.gameObject.SetActive(buttonType == ButtonType.ReloadDiagnosis);
		_reloadMedicationButton.gameObject.SetActive(buttonType == ButtonType.ReloadMedication);
	}
}
