using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PatientDisplay)), RequireComponent(typeof(DialogueController)), RequireComponent(typeof(PatientGenerator))]
public class SymptomsGameController : MonoBehaviour
{
	[SerializeField] private HeartsController _heartsController;

	[Header("UI")]
	[SerializeField] private ScreenSwitcher _screenSwitcher;
	[SerializeField] private GameObject _gameOverMenu;

	[Header("Buttons")]
	[SerializeField] private GreetingButton _greetingButton;
	[SerializeField] private List<DiseaseButton> _diseaseButtons;
	[SerializeField] private Button _reloadButton;
	[SerializeField] private List<MedicationButton> _medicationButtons;
	[SerializeField] private Button _congratulationsButton;

	[Header("Dialogue")]
	[SerializeField] private DialogueTextData _dialogueText;
	[SerializeField] private float _messagesDelay = 0.5f;

	public int DiseaseButtonsCount => _diseaseButtons.Count;
	public int MedicationButtonsCount => _medicationButtons.Count;

	private PatientGenerator _patientGenerator;
	private DialogueController _dialogueController;
	private PatientDisplay _patientDisplay;

	private List<Message> _messages = new();
	private WaitForSeconds _waitForSeconds;
	private Patient _patient;

	public void CheckDiseaseAnswer(DiseaseData disease)
	{
		AddDoctorMessage(disease.Name);

		if (disease.Name == _patientGenerator.Patient.Disease.Name)
		{
			AddPatientMessage(_dialogueText.PatientAskMed, ButtonType.Medication);
		}
		else
		{
			TakeDamage();
		}
	}

	public void CheckMedicationAnswer(MedicationData medication)
	{
		AddDoctorMessage(medication.Name);

		if (_patientGenerator.Patient.IsMedicationFit(medication))
		{
			AddPatientMessage(_dialogueText.PatientThanks, ButtonType.Congratulations);
		}
		else
		{
			TakeDamage();
		}
	}

	public void AddGreetingMessages()
	{
		AddDoctorMessage(_dialogueText.DoctorGreeting);
		AddPatientMessage(_dialogueText.PatientGreeting);

		foreach (string symptom in _patient.SelectedSymptoms)
		{
			AddPatientMessage(symptom);
		}

		AddButtonTypeToLastMessage(ButtonType.Disease);
	}

	public void GenerateNewPatient()
	{
		_patient = _patientGenerator.GeneratePatient();
		_patientDisplay.DisplayPatient(_patient);
		_dialogueController.ResetMessages();
		_messages.Clear();
		SetActiveButtons(ButtonType.Greeting);
	}

	public void InitDisease(int index, DiseaseData disease)
	{
		_diseaseButtons[index].Init(this, disease);
	}

	public void InitMedication(int index, MedicationData medication)
	{
		_medicationButtons[index].Init(this, medication);
	}

	private void TakeDamage()
	{
		_heartsController.TakeDamage();

		if (_heartsController.IsAlive == false)
		{
			GenerateNewPatient();
			_heartsController.ResetHearts();
			_screenSwitcher.SwitchScreen(_gameOverMenu);
		}
		else
		{
			SetActiveButtons(ButtonType.Reload);
		}
	}

	private void AddDoctorMessage(string text, ButtonType buttonType = ButtonType.Null)
	{
		_messages.Add(new Message(text, Sender.Doctor, buttonType));
	}

	private void AddPatientMessage(string text, ButtonType buttonType = ButtonType.Null)
	{
		_messages.Add(new Message(text, Sender.Patient, buttonType));
	}

	private void AddButtonTypeToLastMessage(ButtonType buttonType)
	{
		_messages[_messages.Count - 1].ButtonTypeToActive = buttonType;
	}

	private void Awake()
	{
		_patientDisplay = GetComponent<PatientDisplay>();
		_dialogueController = GetComponent<DialogueController>();
		_patientGenerator = GetComponent<PatientGenerator>();

		_waitForSeconds = new WaitForSeconds(_messagesDelay);
	}

	private void Start()
	{
		GenerateNewPatient();
	}

	private void OnEnable()
	{
		StartCoroutine(AddMessagesCoroutine(_messages));
	}

	private void SetActiveButtons(ButtonType buttonType)
	{
		_greetingButton.gameObject.SetActive(buttonType == ButtonType.Greeting);

		foreach (DiseaseButton button in _diseaseButtons)
		{
			button.gameObject.SetActive(buttonType == ButtonType.Disease);
		}

		_reloadButton.gameObject.SetActive(buttonType == ButtonType.Reload);

		foreach (MedicationButton button in _medicationButtons)
		{
			button.gameObject.SetActive(buttonType == ButtonType.Medication);
		}

		_congratulationsButton.gameObject.SetActive(buttonType == ButtonType.Congratulations);
	}

	private IEnumerator AddMessagesCoroutine(List<Message> messages)
	{
		while (true)
		{
			if (messages.Count > 0)
			{
				_dialogueController.AddMessage(messages[0]);

				if (messages[0].ButtonTypeToActive != ButtonType.Null)
				{
					SetActiveButtons(messages[0].ButtonTypeToActive);
				}

				messages.RemoveAt(0);

				yield return _waitForSeconds;
			}

			yield return null;
		}
	}
}
