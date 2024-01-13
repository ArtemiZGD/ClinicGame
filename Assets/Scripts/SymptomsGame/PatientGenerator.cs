using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using Random = UnityEngine.Random;

// Класс для генерации информации о пациенте
public class PatientGenerator : MonoBehaviour
{
	[Header("Names")]
	[SerializeField] private List<string> _firstNamesM = new List<string>();  // Список мужских имен
	[SerializeField] private List<string> _firstNamesF = new List<string>();  // Список женских имен
	[SerializeField] private List<string> _secondNamesM = new List<string>();  // Список мужских фамилий
	[SerializeField] private List<string> _secondNamesF = new List<string>();  // Список женских фамилий

	[Header("Age")]
	[SerializeField] private int _minAge = 8;  // Минимальный возраст пациента
	[SerializeField] private int _maxAge = 90;  // Максимальный возраст пациента

	[Header("Scripts")]
	[SerializeField] private SymptomsGameController _gameController;  // Контроллер игры с симптомами

	public Patient Patient => _patient;  // Информация о текущем пациенте

	private List<DiseaseData> _diseases;  // Список данных о заболеваниях
	private List<MedicationData> _medications;  // Список данных о лекарствах
	private Patient _patient;  // Текущий пациент

	// Генерация информации о пациенте
	public Patient GeneratePatient()
	{
		_patient = new Patient();
		_patient.Gender = (Gender)Random.Range((int)Gender.Male, (int)Gender.Female + 1);
		_patient.FullName = GenerateRandomFullName(_patient.Gender);
		_patient.Age = Random.Range(_minAge, _maxAge + 1);
		_patient.Disease = _diseases[Random.Range(0, _diseases.Count)];

		_patient.SelectedSymptoms = GetRandomUniqueSymptoms(_patient.Disease, _gameController.DiseaseButtonsCount);

		SetDiseasesButtons();
		SetMedicationButtons();

		return _patient;
	}

	// Загрузка данных о заболеваниях при запуске
	private void Awake()
	{
		_diseases = LoadDiseaseDataFromFolder("ScriptableObjects/Diseases");
		_medications = LoadMedicationDataFromFolder("ScriptableObjects/Medications");
	}

	// Установка кнопок с заболеваниями
	private void SetDiseasesButtons()
	{
		List<DiseaseData> freeDiseases = new List<DiseaseData>(_diseases);
		int diseaseButtonCount = _gameController.DiseaseButtonsCount;

		int correctButtonNumber = Random.Range(0, diseaseButtonCount);
		freeDiseases.Remove(_patient.Disease);

		for (int i = 0; i < diseaseButtonCount; i++)
		{
			if (i == correctButtonNumber)
			{
				_gameController.InitDisease(i, _patient.Disease);
			}
			else
			{
				DiseaseData disease = freeDiseases[Random.Range(0, freeDiseases.Count)];
				freeDiseases.Remove(disease);
				_gameController.InitDisease(i, disease);
			}
		}

		Debug.Log(_patient.Disease.Name);
	}

	// Установка кнопок с лекарствами
	private void SetMedicationButtons()
	{
		List<MedicationData> correctMedications = new List<MedicationData>(_patient.Disease.Medications);
		MedicationData correctMedication = correctMedications[Random.Range(0, correctMedications.Count)];

		List<MedicationData> wrongMedications = new List<MedicationData>();

		foreach (MedicationData medication in _medications)
		{
			if (!correctMedications.Contains(medication))
			{
				wrongMedications.Add(medication);
			}
		}

		int medicationButtonsCount = _gameController.MedicationButtonsCount;
		int correctButtonNumber = Random.Range(0, medicationButtonsCount);

		for (int i = 0; i < medicationButtonsCount; i++)
		{
			if (i == correctButtonNumber)
			{
				_gameController.InitMedication(i, correctMedication);
			}
			else
			{
				MedicationData medication = wrongMedications[Random.Range(0, wrongMedications.Count)];
				wrongMedications.Remove(medication);
				_gameController.InitMedication(i, medication);
			}
		}

		Debug.Log(correctMedication);
	}

	// Получение случайных уникальных симптомов
	private List<string> GetRandomUniqueSymptoms(DiseaseData disease, int diseaseButtonsCount)
	{
		List<SymptomData> allSymptoms = disease.Symptoms.ToList();
		List<SymptomData> selectedSymptoms = new List<SymptomData>();

		int minAttempts = Mathf.Min(diseaseButtonsCount, allSymptoms.Count);
		int attempts = 0;

		while (GetDiseasesCount(selectedSymptoms) != 1 || attempts < minAttempts)
		{
			SymptomData symptomData = allSymptoms[Random.Range(0, allSymptoms.Count)]; //Out of range
			selectedSymptoms.Add(symptomData);
			allSymptoms.Remove(symptomData);

			if (GetDiseasesCount(selectedSymptoms) < 1)
			{
				Debug.LogError("No matching diseases found");
			}

			attempts++;
		}

		List<string> selectedSymptomNames = new List<string>();

		foreach (var symptom in selectedSymptoms)
		{
			selectedSymptomNames.Add(symptom.Name);
		}

		return selectedSymptomNames;
	}

	// Получение количества совпадающих заболеваний по симптомам
	private int GetDiseasesCount(List<SymptomData> selectedSymptoms)
	{
		int diseasesCount = 0;

		foreach (DiseaseData disease in _diseases)
		{
			if (selectedSymptoms.All(symptom => disease.Symptoms.Contains(symptom)))
			{
				diseasesCount++;
			}
		}

		return diseasesCount;
	}

	// Загрузка данных о заболеваниях из указанной папки
	private List<DiseaseData> LoadDiseaseDataFromFolder(string folderPath)
	{
		List<DiseaseData> diseases = new List<DiseaseData>();

		DiseaseData[] diseaseDataArray = Resources.LoadAll<DiseaseData>(folderPath);

		diseases.AddRange(diseaseDataArray);

		return diseases;
	}

	// Загрузка данных о лекарствах из указанной папки
	private List<MedicationData> LoadMedicationDataFromFolder(string folderPath)
	{
		List<MedicationData> medications = new List<MedicationData>();

		MedicationData[] medicationDataArray = Resources.LoadAll<MedicationData>(folderPath);

		medications.AddRange(medicationDataArray);

		return medications;
	}

	// Генерация случайного полного имени в зависимости от пола
	private string GenerateRandomFullName(Gender gender)
	{
		string firstNames;
		string secondNames;

		if (gender == Gender.Male)
		{
			firstNames = _firstNamesM[Random.Range(0, _firstNamesM.Count)];
			secondNames = _secondNamesM[Random.Range(0, _secondNamesM.Count)];
		}
		else
		{
			firstNames = _firstNamesF[Random.Range(0, _firstNamesF.Count)];
			secondNames = _secondNamesF[Random.Range(0, _secondNamesF.Count)];
		}

		return secondNames + " " + firstNames;
	}
}
