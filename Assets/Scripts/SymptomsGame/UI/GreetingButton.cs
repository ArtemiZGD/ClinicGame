using TMPro;
using UnityEngine;

public class GreetingButton : MonoBehaviour
{
	[SerializeField] private DialogueTextData _dialogueTextData;
    [SerializeField] private TMP_Text _text;

	private void Start()
	{
		_text.text = _dialogueTextData.DoctorGreeting;
	}
}
