using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

// Класс, представляющий информацию о пациенте
public class Patient
{
	public string FullName;  // Полное имя пациента
	public int Age;  // Возраст пациента
	public Gender Gender;  // Пол пациента
	public Sprite Sprite;  // Спрайт пациента
	public DiseaseData Disease;  // Информация о заболевании пациента
	public List<string> SelectedSymptoms = new List<string>();  // Список выбранных симптомов пациента

	// Проверка, подходит ли лекарство пациенту
	public bool IsMedicationFit(MedicationData medication)
	{
		foreach (MedicationData correctMedication in Disease.Medications)
		{
			if (correctMedication.Name == medication.Name)
			{
				return true;  // Лекарство соответствует заболеванию пациента
			}
		}

		return false;  // Лекарство не соответствует заболеванию пациента
	}
}
