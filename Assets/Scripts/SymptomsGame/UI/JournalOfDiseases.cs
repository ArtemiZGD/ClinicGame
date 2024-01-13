using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class JournalOfDiseases : MonoBehaviour
{
	[SerializeField] private Button _previousButton;  // Кнопка для отображения предыдущей болезни
	[SerializeField] private Button _nextButton;  // Кнопка для отображения следующей болезни
	[SerializeField] private MessageObject _diseaseMessage;  // Объект для отображения сообщения о болезни
	[SerializeField] private TMP_Text _symptomsText;  // Текст для отображения симптомов болезни
	[SerializeField] private TMP_Text _medicationsText;  // Текст для отображения лекарств болезни

	private List<DiseaseData> _diseases;  // Список данных о болезнях
	private int _currentDiseaseIndex;  // Индекс текущей болезни

	// Вызывается при старте объекта
	private void Start()
	{
		_diseases = LoadDiseaseDataFromFolder("ScriptableObjects/Diseases");  // Загрузка данных о болезнях из ресурсов

		if (_diseases.Count > 0)
		{
			_currentDiseaseIndex = 0;
			UpdateUI();  // Обновление интерфейса с текущей болезнью
		}

		_previousButton.onClick.AddListener(ShowPreviousDisease);  // Привязка метода к событию нажатия кнопки "Предыдущая болезнь"
		_nextButton.onClick.AddListener(ShowNextDisease);  // Привязка метода к событию нажатия кнопки "Следующая болезнь"
	}

	// Загрузка данных о болезнях из указанной папки
	private List<DiseaseData> LoadDiseaseDataFromFolder(string folderPath)
	{
		List<DiseaseData> diseaseList = new List<DiseaseData>();

		DiseaseData[] diseaseDataArray = Resources.LoadAll<DiseaseData>(folderPath);

		diseaseList.AddRange(diseaseDataArray);

		return diseaseList;
	}

	// Отображение предыдущей болезни
	private void ShowPreviousDisease()
	{
		if (_diseases.Count == 0)
			return;

		_currentDiseaseIndex = (_currentDiseaseIndex - 1 + _diseases.Count) % _diseases.Count;
		UpdateUI();  // Обновление интерфейса с новой болезнью
	}

	// Отображение следующей болезни
	private void ShowNextDisease()
	{
		if (_diseases.Count == 0)
			return;

		_currentDiseaseIndex = (_currentDiseaseIndex + 1) % _diseases.Count;
		UpdateUI();  // Обновление интерфейса с новой болезнью
	}

	// Обновление интерфейса с текущей болезнью
	private void UpdateUI()
	{
		DiseaseData currentDisease = _diseases[_currentDiseaseIndex];
		_diseaseMessage.Type(currentDisease.Name);  // Отображение сообщения о текущей болезни

		_symptomsText.text = "";
		_medicationsText.text = "";

		foreach (var symptom in currentDisease.Symptoms)
		{
			_symptomsText.text += "\n-" + symptom.Name;  // Добавление симптомов в текст
		}

		foreach (var medication in currentDisease.Medications)
		{
			_medicationsText.text += "\n-" + medication.Name;  // Добавление лекарств в текст
		}
	}
}
