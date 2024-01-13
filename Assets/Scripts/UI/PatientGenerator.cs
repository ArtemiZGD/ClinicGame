using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PatientGenerator : MonoBehaviour
{
	[Header("Names")]
	[SerializeField] private List<string> _firstNames = new();
	[SerializeField] private List<string> _secondNames = new();

	[Header("Icons")]
	[SerializeField] private Sprite _patient_8_25_M_Image;
	[SerializeField] private Sprite _patient_8_25_F_Image;
	[SerializeField] private Sprite _patient_26_60_M_Image;
	[SerializeField] private Sprite _patient_26_60_F_Image;
	[SerializeField] private Sprite _patient_61_90_M_Image;
	[SerializeField] private Sprite _patient_61_90_F_Image;

	[Header("Age")]
	[SerializeField] private int _minAge = 8;
	[SerializeField] private int _maxAge = 90;
	[SerializeField] private int _currentYear = 2023;

	[Header("UI")]
	[SerializeField] private Image _patientIcon;
	[SerializeField] private TMP_Text _patientInfoText;
	[SerializeField] private TMP_Text _symptomsText;
	[SerializeField] private TMP_InputField _deseaseInput;
	[SerializeField] private TMP_InputField _medicationsInput;
	[SerializeField] private List<Image> _hearts = new();
	[SerializeField] private Color _brokenHeartColor = Color.black;
	[SerializeField] private Color _fullHeartColor = Color.white;
	[SerializeField] private string _stringToJoin = ";";

	[SerializeField] private Vector2Int _symptomsCountRange = new(3, 3);

	[Header("Menu")]
	[SerializeField] private GameObject _gameOverMenu;
	[SerializeField] private ScreenSwitcher ScreenSwitcher;

	private List<DiseaseData> _diseases;
	private Patient _patient;
	private int _hp;

	public void CheckAnswer()
	{
		bool isDeseaseCorrect = _deseaseInput.text.ToLower() == _patient.Disease.Name.ToLower();

		if (isDeseaseCorrect)
		{
			List<string> medicationsInput = SplitString(_medicationsInput.text);
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
					HealthDown();
					Debug.Log("Не верные препараты");
				}
			}
			else
			{
				HealthDown();
				Debug.Log("Не верные препараты");
			}
		}
		else
		{
			HealthDown();
			Debug.Log("Не верная болезнь");
		}
	}

	public void GeneratePatient()
	{
		_deseaseInput.text = "";
		_medicationsInput.text = "";

		_patient = new Patient();
		_patient.FullName = GenerateRandomFullName();
		_patient.Age = Random.Range(_minAge, _maxAge + 1);
		_patient.Gender = (Gender)Random.Range((int)Gender.Male, (int)Gender.Female + 1);
		_patient.Sprite = GetPatienImage(_patient.Gender, _patient.Age);
		_patient.Disease = _diseases[Random.Range(0, _diseases.Count)];

		_patient.SelectedSymptoms = GetRandomUniqueSymptoms(_patient.Disease);

		DisplayPatient();

		Debug.Log(_patient.Disease.Name);
	}

	private void Start()
	{
		if (_symptomsCountRange.x > _symptomsCountRange.y)
			Debug.LogError("Wrong SymptomsCountRange");

		_diseases = LoadDiseaseDataFromFolder("ScriptableObjects/Diseases");
		GeneratePatient();

		_hp = _hearts.Count();
	}

	private void HealthDown()
	{
		_hp--;
		SetHealth();

		if (_hp == 0)
		{
			GameOver();
		}
	}

	private void GameOver()
	{
		_hp = _hearts.Count;
		SetHealth();
		GeneratePatient();
		ScreenSwitcher.SwitchObjects(_gameOverMenu);
	}

	private void SetHealth()
	{
		for (int i = 0; i < _hearts.Count; i++)
		{
			if (i < _hp)
			{
				_hearts[i].color = _fullHeartColor;
			}
			else
			{
				_hearts[i].color = _brokenHeartColor;
			}
		}
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

	private void DisplayPatient()
	{
		string genderRus = "Null";

		if (_patient.Gender == Gender.Male)
		{
			genderRus = "М";
		}
		else if (_patient.Gender == Gender.Female)
		{
			genderRus = "Ж";
		}
		else
		{
			Debug.LogError("Not polite gender");
		}

		_patientInfoText.text = $"{_patient.FullName}, {_currentYear - _patient.Age}, {genderRus}";
		_symptomsText.text = string.Join($"{_stringToJoin}\n", _patient.SelectedSymptoms);
		_symptomsText.text += _stringToJoin;
		_patientIcon.sprite = _patient.Sprite;
	}

	private List<string> GetRandomUniqueSymptoms(DiseaseData disease)
	{
		List<SymptomData> allSymptoms = disease.Symptoms.ToList();
		List<SymptomData> selectedSymptoms = new();

		int minAttempts = Mathf.Min(Random.Range(_symptomsCountRange.x, _symptomsCountRange.y), allSymptoms.Count);
		int attempts = 0;

		do
		{
			if (allSymptoms.Count == 0)
			{
				Debug.LogError("The symptoms aren't clear-cut");
			}

			SymptomData symptomData = allSymptoms[Random.Range(0, allSymptoms.Count)];
			selectedSymptoms.Add(symptomData);
			allSymptoms.Remove(symptomData);

			if (GetDiseasesCount(selectedSymptoms) < 1)
			{
				Debug.LogError("No matching diseases found");
			}

			attempts++;
		} while (GetDiseasesCount(selectedSymptoms) != 1 || (attempts <= minAttempts && allSymptoms.Count != 0));

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
