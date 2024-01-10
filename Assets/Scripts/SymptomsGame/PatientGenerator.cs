using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using Random = UnityEngine.Random;

public class PatientGenerator : MonoBehaviour
{
	[Header("Names")]
	[SerializeField] private List<string> _firstNamesM = new();
	[SerializeField] private List<string> _firstNamesF = new();
	[SerializeField] private List<string> _secondNamesM = new();
	[SerializeField] private List<string> _secondNamesF = new();

	[Header("Age")]
	[SerializeField] private int _minAge = 8;
	[SerializeField] private int _maxAge = 90;

	[Header("Scripts")]
	[SerializeField] private SymptomsGameController _gameController;

	public Patient Patient => _patient;

	private List<DiseaseData> _diseases;
	private List<MedicationData> _medications;
	private Patient _patient;

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

	private void Awake()
	{
		_diseases = LoadDiseaseDataFromFolder("ScriptableObjects/Diseases");
		_medications = LoadMedicationDataFromFolder("ScriptableObjects/Medications");
	}

	private void SetDiseasesButtons()
	{
		List<DiseaseData> freeDiseases = new(_diseases);
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

	private void SetMedicationButtons()
	{
		List<MedicationData> correctMedications = new(_patient.Disease.Medications);
		MedicationData correctMedication = correctMedications[Random.Range(0, correctMedications.Count)];

		List<MedicationData> wrongMedications = new();

		foreach (MedicationData medication in _medications)
		{
			if (correctMedications.Contains(medication) == false)
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

	private List<string> GetRandomUniqueSymptoms(DiseaseData disease, int diseaseButtonsCount)
	{
		List<SymptomData> allSymptoms = disease.Symptoms.ToList();
		List<SymptomData> selectedSymptoms = new();

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

		List<string> selectedSymptomNames = new();

		foreach (var symptom in selectedSymptoms)
		{
			selectedSymptomNames.Add(symptom.Name);
		}

		return selectedSymptomNames;
	}

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

	private List<DiseaseData> LoadDiseaseDataFromFolder(string folderPath)
	{
		List<DiseaseData> diseases = new();

		DiseaseData[] diseaseDataArray = Resources.LoadAll<DiseaseData>(folderPath);

		diseases.AddRange(diseaseDataArray);

		return diseases;
	}

	private List<MedicationData> LoadMedicationDataFromFolder(string folderPath)
	{
		List<MedicationData> medications = new();

		MedicationData[] medicationDataArray = Resources.LoadAll<MedicationData>(folderPath);

		medications.AddRange(medicationDataArray);

		return medications;
	}

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
