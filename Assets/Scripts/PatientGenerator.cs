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
	[SerializeField] private List<string> _firstNames = new();
	[SerializeField] private List<string> _secondNames = new();

	[Header("Age")]
	[SerializeField] private int _minAge = 8;
	[SerializeField] private int _maxAge = 90;

	[Header("Symptoms")]
	[SerializeField] private Vector2Int _symptomsCountRange = new(3, 3);

	[Header("Display")]
	[SerializeField] private PatientDisplay _patientDisplay;

	private List<DiseaseData> _diseases;
	private Patient _patient;

	public void CheckAnswer(string desease, string medications)
	{
		bool isDeseaseCorrect = desease.ToLower() == _patient.Disease.Name.ToLower();

		if (isDeseaseCorrect)
		{
			List<string> medicationsInput = SplitString(medications);
			List<string> medicationsCorrect = new();

			foreach (var medication in _patient.Disease.Medications)
			{
				medicationsCorrect.Add(medication.Name);
			}

			medicationsInput.Sort();
			medicationsCorrect.Sort();

			if (medicationsCorrect.Count == medicationsInput.Count)
			{
				bool isCorrect = true;

				for (int i = 0; i < medicationsInput.Count; i++)
				{
					if (medicationsInput[i].ToLower() != medicationsCorrect[i].ToLower())
						isCorrect = false;
				}

				if (isCorrect)
				{
					GeneratePatient();
				}
				else
				{
					Debug.Log("Не верные препараты");
				}
			}
			else
			{
				Debug.Log("Не верные препараты");
			}
		}
		else
		{
			Debug.Log("Не верная болезнь");
		}
	}

	public void GeneratePatient()
	{
		_patient = new Patient();
		_patient.FullName = GenerateRandomFullName();
		_patient.Age = Random.Range(_minAge, _maxAge + 1);
		_patient.Gender = (Gender)Random.Range((int)Gender.Male, (int)Gender.Female + 1);
		_patient.Disease = _diseases[Random.Range(0, _diseases.Count)];

		_patient.SelectedSymptoms = GetRandomUniqueSymptoms(_patient.Disease);

		_patientDisplay.DisplayPatient(_patient);

		Debug.Log(_patient.Disease.Name);
	}

	private void Start()
	{
		if (_symptomsCountRange.x > _symptomsCountRange.y)
			Debug.LogError("Wrong SymptomsCountRange");

		_diseases = LoadDiseaseDataFromFolder("ScriptableObjects/Diseases");
		GeneratePatient();
	}

	private List<string> SplitString(string str)
	{
		List<string> strings = str.Split(',', '.', '\n').ToList();

		for (int i = strings.Count - 1; i >= 0; i--)
		{
			if (strings[i].Trim() == "")
			{
				strings.RemoveAt(i);
			}
		}

		return strings;
	}

	private List<string> GetRandomUniqueSymptoms(DiseaseData disease)
	{
		List<SymptomData> allSymptoms = disease.Symptoms.ToList();
		List<SymptomData> selectedSymptoms = new();

		int minAttempts = Mathf.Min(Random.Range(_symptomsCountRange.x, _symptomsCountRange.y), allSymptoms.Count);
		int attempts = 0;

		do
		{
			SymptomData symptomData = allSymptoms[Random.Range(0, allSymptoms.Count)];
			selectedSymptoms.Add(symptomData);
			allSymptoms.Remove(symptomData);

			if (GetDiseasesCount(selectedSymptoms) < 1)
			{
				Debug.LogError("No matching diseases found");
			}

			attempts++;
		} while (GetDiseasesCount(selectedSymptoms) != 1 || attempts <= minAttempts);

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
		List<DiseaseData> diseaseList = new();

		DiseaseData[] diseaseDataArray = Resources.LoadAll<DiseaseData>(folderPath);

		diseaseList.AddRange(diseaseDataArray);

		return diseaseList;
	}
	
	private string GenerateRandomFullName()
	{
		string firstNames = _firstNames[Random.Range(0, _firstNames.Count)];
		string secondNames = _secondNames[Random.Range(0, _secondNames.Count)];

		return secondNames + " " + firstNames;
	}
}
