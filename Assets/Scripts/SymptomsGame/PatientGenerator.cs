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
    [SerializeField] private PatientDisplay _patientDisplay;
    [SerializeField] private SymptomsGameController _gameController;

    [Header("UI")]
    [SerializeField] private List<DiseaseButton> _diseaseButtons = new();

    public Patient Patient => _patient;

    private List<DiseaseData> _diseases;
    private Patient _patient;

    public void GeneratePatient()
    {
        _patient = new Patient();
        _patient.Gender = (Gender)Random.Range((int)Gender.Male, (int)Gender.Female + 1);
        _patient.FullName = GenerateRandomFullName(_patient.Gender);
        _patient.Age = Random.Range(_minAge, _maxAge + 1);
        _patient.Disease = _diseases[Random.Range(0, _diseases.Count)];

        _patient.SelectedSymptoms = GetRandomUniqueSymptoms(_patient.Disease);

        _patientDisplay.DisplayPatient(_patient);
        SetDiseasesButtons();

        Debug.Log(_patient.Disease.Name);
    }

    private void Start()
    {
        _diseases = LoadDiseaseDataFromFolder("ScriptableObjects/Diseases");
        GeneratePatient();
    }

    private void SetDiseasesButtons()
    {
        List<DiseaseData> freeDiseases = new(_diseases);

        int correctButtonNumber = Random.Range(0, _diseaseButtons.Count);
        freeDiseases.Remove(_patient.Disease);

        for (int i = 0; i < _diseaseButtons.Count; i++)
        {
            if (i == correctButtonNumber)
            {
                _diseaseButtons[i].Init(_gameController, _patient.Disease);
            }
            else
            {
                DiseaseData disease = freeDiseases[Random.Range(0, freeDiseases.Count)];
                freeDiseases.Remove(disease);
                _diseaseButtons[i].Init(_gameController, disease);
            }
        }
    }

    private List<string> GetRandomUniqueSymptoms(DiseaseData disease)
    {
        List<SymptomData> allSymptoms = disease.Symptoms.ToList();
        List<SymptomData> selectedSymptoms = new();

        int minAttempts = Mathf.Min(_diseaseButtons.Count, allSymptoms.Count);
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
        List<DiseaseData> diseaseList = new();

        DiseaseData[] diseaseDataArray = Resources.LoadAll<DiseaseData>(folderPath);

        diseaseList.AddRange(diseaseDataArray);

        return diseaseList;
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
