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
}
