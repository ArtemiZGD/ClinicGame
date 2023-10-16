using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PatientGenerator : MonoBehaviour
{
	[SerializeField] private List<string> _firstNames = new();
	[SerializeField] private List<string> _secondNames = new();

	[SerializeField] private Sprite _patient_8_25_M_Image;
	[SerializeField] private Sprite _patient_8_25_F_Image;
	[SerializeField] private Sprite _patient_26_60_M_Image;
	[SerializeField] private Sprite _patient_26_60_F_Image;
	[SerializeField] private Sprite _patient_61_90_M_Image;
	[SerializeField] private Sprite _patient_61_90_F_Image;

	[SerializeField] private int _minAge = 8;
	[SerializeField] private int _maxAge = 90;
	[SerializeField] private int _currentYear = 2023;

	[SerializeField] private Image _patientIcon;
	[SerializeField] private TMP_Text _patientInfoText;
	[SerializeField] private TMP_Text _symptomsText;

	[SerializeField] private Vector2Int _symptomsCountRange = new(3, 3);

	private List<DiseaseData> _diseases;

	private void Start()
	{
		_diseases = LoadDiseaseDataFromFolder("ScriptableObjects/Diseases");
		GeneratePatient();
	}

	public void GeneratePatient()
	{
		string fullName = GenerateRandomFullName();
		Gender gender = (Gender)Random.Range((int)Gender.Male, (int)Gender.Female + 1);
		int age = Random.Range(_minAge, _maxAge + 1);
		Sprite image = GetPatienImage(gender, age);
		DiseaseData disease = _diseases[Random.Range(0, _diseases.Count)];

		List<string> selectedSymptoms = GetRandomUniqueSymptoms(disease);

		string genderRus = "Null";

		if (gender == Gender.Male)
		{
			genderRus = "Ì";
		}
		else if (gender == Gender.Female)
		{
			genderRus = "Æ";
		}
		else
		{
			Debug.LogError("Not polite gender");
		}

		_patientInfoText.text = $"{fullName}, {_currentYear - age}, {genderRus}";
		_symptomsText.text = string.Join("\n", selectedSymptoms);
		_patientIcon.sprite = image;
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


	private Sprite GetPatienImage(Gender gender, int age)
	{
		Sprite image = null;

		if (gender == Gender.Male)
		{
			if (age >= 8 && age <= 25)
			{
				image = _patient_8_25_M_Image;
			}
			else if (age >= 26 && age <= 60)
			{
				image = _patient_26_60_M_Image;
			}
			else if (age >= 61 && age <= 90)
			{
				image = _patient_61_90_M_Image;
			}
		}
		else if (gender == Gender.Female)
		{

			if (age >= 8 && age <= 25)
			{
				image = _patient_8_25_F_Image;
			}
			else if (age >= 26 && age <= 60)
			{
				image = _patient_26_60_F_Image;
			}
			else if (age >= 61 && age <= 90)
			{
				image = _patient_61_90_F_Image;
			}
		}

		return image;
	}

	private string GenerateRandomFullName()
	{
		string firstNames = _firstNames[Random.Range(0, _firstNames.Count)];
		string secondNames = _secondNames[Random.Range(0, _secondNames.Count)];

		return secondNames + " " + firstNames;
	}
}
