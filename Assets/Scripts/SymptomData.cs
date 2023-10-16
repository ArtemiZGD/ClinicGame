using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Symptom", menuName = "Custom/Symptom")]
public class SymptomData : ScriptableObject
{
	public List<DiseaseData> DiseaseData = new();
	public string Name;
}
