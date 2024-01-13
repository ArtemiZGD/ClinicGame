using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class PatientDisplay : MonoBehaviour
{
	[Header("Icons")]
	[SerializeField] private Sprite _patient_8_25_M_Image;
	[SerializeField] private Sprite _patient_8_25_F_Image;
	[SerializeField] private Sprite _patient_26_60_M_Image;
	[SerializeField] private Sprite _patient_26_60_F_Image;
	[SerializeField] private Sprite _patient_61_90_M_Image;
	[SerializeField] private Sprite _patient_61_90_F_Image;

	[Header("UI")]
	[SerializeField] private Image _patientIcon;  // Èêîíêà ïàöèåíòà
	[SerializeField] private TMP_Text _patientInfoText;  // Òåêñòîâîå ïîëå äëÿ îòîáğàæåíèÿ èíôîğìàöèè î ïàöèåíòå

	[Header("Year")]
	[SerializeField] private int _currentYear = 2024;  // Òåêóùèé ãîä

	// Îòîáğàæåíèå èíôîğìàöèè î ïàöèåíòå
	public void DisplayPatient(Patient patient)
	{
		string genderRus = "Null";

		if (patient.Gender == Gender.Male)
		{
			genderRus = "Ì";
		}
		else if (patient.Gender == Gender.Female)
		{
			genderRus = "Æ";
		}
		else
		{
			Debug.LogError("Not polite gender");
		}

		patient.Sprite = GetPatientImage(patient.Gender, patient.Age);
		_patientInfoText.text = $"ÔÈÎ: {patient.FullName}\nÃîä ğîæäåíèÿ: {_currentYear - patient.Age}\nÏîë: {genderRus}";
		_patientIcon.sprite = patient.Sprite;
	}

	// Ïîëó÷åíèå èçîáğàæåíèÿ ïàöèåíòà â çàâèñèìîñòè îò ïîëà è âîçğàñòà
	private Sprite GetPatientImage(Gender gender, int age)
	{
		Sprite image = null;

		if (gender == Gender.Male)
		{
			if (age >= 8 && age <= 25)
			{
				image = _patient_8_25_M_Image;
			}
			else if (age >= 26 && age <= 60)
			{
				image = _patient_26_60_M_Image;
			}
			else if (age >= 61 && age <= 90)
			{
				image = _patient_61_90_M_Image;
			}
		}
		else if (gender == Gender.Female)
		{
			if (age >= 8 && age <= 25)
			{
				image = _patient_8_25_F_Image;
			}
			else if (age >= 26 && age <= 60)
			{
				image = _patient_26_60_F_Image;
			}
			else if (age >= 61 && age <= 90)
			{
				image = _patient_61_90_F_Image;
			}
		}

		return image;
	}
}
