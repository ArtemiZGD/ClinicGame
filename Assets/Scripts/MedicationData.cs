using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Medication", menuName = "Custom/Medication")]
public class MedicationData : ScriptableObject
{
	public List<DiseaseData> DiseaseData = new();
	public string Name;
}
