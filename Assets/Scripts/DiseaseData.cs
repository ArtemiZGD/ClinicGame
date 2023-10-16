using UnityEngine;

[CreateAssetMenu(fileName = "New Disease", menuName = "Custom/Disease")]
public class DiseaseData : ScriptableObject
{
	public string Name;
	public SymptomData[] Symptoms;
	public MedicationData[] Medications;
}
