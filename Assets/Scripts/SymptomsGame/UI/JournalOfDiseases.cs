using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class JournalOfDiseases : MonoBehaviour
{
	[SerializeField] private Button _previousButton;  // ������ ��� ����������� ���������� �������
	[SerializeField] private Button _nextButton;  // ������ ��� ����������� ��������� �������
	[SerializeField] private MessageObject _diseaseMessage;  // ������ ��� ����������� ��������� � �������
	[SerializeField] private TMP_Text _symptomsText;  // ����� ��� ����������� ��������� �������
	[SerializeField] private TMP_Text _medicationsText;  // ����� ��� ����������� �������� �������

	private List<DiseaseData> _diseases;  // ������ ������ � ��������
	private int _currentDiseaseIndex;  // ������ ������� �������

	// ���������� ��� ������ �������
	private void Start()
	{
		_diseases = LoadDiseaseDataFromFolder("ScriptableObjects/Diseases");  // �������� ������ � �������� �� ��������

		if (_diseases.Count > 0)
		{
			_currentDiseaseIndex = 0;
			UpdateUI();  // ���������� ���������� � ������� ��������
		}

		_previousButton.onClick.AddListener(ShowPreviousDisease);  // �������� ������ � ������� ������� ������ "���������� �������"
		_nextButton.onClick.AddListener(ShowNextDisease);  // �������� ������ � ������� ������� ������ "��������� �������"
	}

	// �������� ������ � �������� �� ��������� �����
	private List<DiseaseData> LoadDiseaseDataFromFolder(string folderPath)
	{
		List<DiseaseData> diseaseList = new List<DiseaseData>();

		DiseaseData[] diseaseDataArray = Resources.LoadAll<DiseaseData>(folderPath);

		diseaseList.AddRange(diseaseDataArray);

		return diseaseList;
	}

	// ����������� ���������� �������
	private void ShowPreviousDisease()
	{
		if (_diseases.Count == 0)
			return;

		_currentDiseaseIndex = (_currentDiseaseIndex - 1 + _diseases.Count) % _diseases.Count;
		UpdateUI();  // ���������� ���������� � ����� ��������
	}

	// ����������� ��������� �������
	private void ShowNextDisease()
	{
		if (_diseases.Count == 0)
			return;

		_currentDiseaseIndex = (_currentDiseaseIndex + 1) % _diseases.Count;
		UpdateUI();  // ���������� ���������� � ����� ��������
	}

	// ���������� ���������� � ������� ��������
	private void UpdateUI()
	{
		DiseaseData currentDisease = _diseases[_currentDiseaseIndex];
		_diseaseMessage.Type(currentDisease.Name);  // ����������� ��������� � ������� �������

		_symptomsText.text = "";
		_medicationsText.text = "";

		foreach (var symptom in currentDisease.Symptoms)
		{
			_symptomsText.text += "\n-" + symptom.Name;  // ���������� ��������� � �����
		}

		foreach (var medication in currentDisease.Medications)
		{
			_medicationsText.text += "\n-" + medication.Name;  // ���������� �������� � �����
		}
	}
}
