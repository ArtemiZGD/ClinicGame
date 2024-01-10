using UnityEngine;

[CreateAssetMenu(fileName = "New dialogue text", menuName = "Custom/DialogueText")]
public class DialogueTextData : ScriptableObject
{
	[TextArea]
	public string DoctorGreeting;
	[TextArea]
	public string PatientGreeting;
	[TextArea]
	public string PatientAskMed;
	[TextArea]
	public string PatientThanks;
}
