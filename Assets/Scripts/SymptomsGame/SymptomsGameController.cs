using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PatientDisplay)), RequireComponent(typeof(PatientGenerator))]
public class SymptomsGameController : MonoBehaviour
{
	[SerializeField] private HeartsController _heartsController;  // Контроллер жизней
	[SerializeField] private int _maxScore = 5;  // Максимальное количество очков

	[Header("UI")]
	[SerializeField] private ScreenSwitcher _screenSwitcher;  // Переключатель экранов
	[SerializeField] private GameObject _gameOverMenu;  // Меню окончания игры
	[SerializeField] private GameObject _victoryMenu;  // Меню победы
	[SerializeField] private TMP_Text _symptomsText;  // Текст с симптомами
	[SerializeField] private string _symptomsTextBeginning;  // Начальный текст симптомов
	[SerializeField] private char _symptomsTextSeparator = '-';  // Разделитель симптомов
	[SerializeField] private TMP_Text _diagnosisText;  // Текст с диагнозом
	[SerializeField] private TMP_Text _medicationText;  // Текст с лекарством

	[Header("Buttons")]
	[SerializeField] private List<DiseaseButton> _diseaseButtons;  // Кнопки с заболеваниями
	[SerializeField] private WrongDiseaseController _reloadDiagnosisButton;  // Кнопка перезагрузки диагноза
	[SerializeField] private WrongMedicationController _reloadMedicationButton;  // Кнопка перезагрузки лекарства
	[SerializeField] private List<MedicationButton> _medicationButtons;  // Кнопки с лекарствами

	public int DiseaseButtonsCount => _diseaseButtons.Count;  // Количество кнопок с заболеваниями
	public int MedicationButtonsCount => _medicationButtons.Count;  // Количество кнопок с лекарствами
	public int Score => _score;  // Количество очков
	public int MaxScore => _maxScore;  // Максимальное количество очков

	private PatientGenerator _patientGenerator;  // Генератор пациентов
	private PatientDisplay _patientDisplay;  // Отображение информации о пациенте

	private Patient _patient;  // Текущий пациент

	private int _score;  // Очки игрока

	// Проверка ответа по заболеванию
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

	// Проверка ответа по лекарству
	public void CheckMedicationAnswer(MedicationData medication)
	{
		if (_patientGenerator.Patient.IsMedicationFit(medication))
		{
			_medicationText.text = medication.Name;
			_score++;

			if (_score == _maxScore)
			{
				_heartsController.ResetHearts();
				_screenSwitcher.SwitchScreen(_victoryMenu);
				_score = 0;
			}
			else
			{
				GenerateNewPatient();
			}
		}
		else
		{
			TakeDamage(ButtonType.ReloadMedication);
			_reloadMedicationButton.SetTexts(_patient.Disease);
		}
	}

	// Генерация нового пациента
	public void GenerateNewPatient()
	{
		_patient = _patientGenerator.GeneratePatient();
		_patientDisplay.DisplayPatient(_patient);
		SetActiveButtons(ButtonType.Disease);
		DisplaySymptoms();
		ClearTexts();
	}

	// Инициализация кнопки с заболеванием
	public void InitDisease(int index, DiseaseData disease)
	{
		_diseaseButtons[index].Init(this, disease);
	}

	// Инициализация кнопки с лекарством
	public void InitMedication(int index, MedicationData medication)
	{
		_medicationButtons[index].Init(this, medication);
	}

	// Инициализация при старте
	private void Awake()
	{
		_patientDisplay = GetComponent<PatientDisplay>();
		_patientGenerator = GetComponent<PatientGenerator>();
	}

	// Инициализация при запуске
	private void Start()
	{
		_score = 0;
		GenerateNewPatient();
	}

	// Отображение симптомов
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

	// Очистка текстов
	public void ClearTexts()
	{
		_diagnosisText.text = "";
		_medicationText.text = "";
	}

	// Получение урона
	private void TakeDamage(ButtonType buttonType)
	{
		_heartsController.TakeDamage();

		if (!_heartsController.IsAlive)
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

	// Активация кнопок определенного типа
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
