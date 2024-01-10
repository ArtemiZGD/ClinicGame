using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Patient
{
	public string FullName;
	public int Age;
	public Gender Gender;
	public Sprite Sprite;
	public DiseaseData Disease;
	public List<string> SelectedSymptoms = new();

	public bool IsMedicationFit(MedicationData medication)
	{
		foreach (MedicationData correctMedication in Disease.Medications)
		{
			if (correctMedication.Name == medication.Name)
			{
				return true;
			}
		}

		return false;
	}
}
