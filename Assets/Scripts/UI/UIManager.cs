using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] private GameObject _mainMenu;
	[SerializeField] private GameObject _choiceMenu;
	[SerializeField] private GameObject _patientEntryMenu;
	[SerializeField] private GameObject _doctorEntryMenu;
	[SerializeField] private GameObject _patientCreateMenu;
	[SerializeField] private GameObject _doctorCreateMenu;
	[SerializeField] private GameObject _patientChoiceMenu;
	[SerializeField] private GameObject _doctorChoiceMenu;
	[SerializeField] private GameObject _patientMenu;
	[SerializeField] private GameObject _doctorMenu;
	[SerializeField] private GameObject _adminMenu;

	private void Awake()
	{
		ActivateMenu(_mainMenu);
	}

	private void ActivateMenu(GameObject menuToActivate)
	{
		_mainMenu.SetActive(false);
		_choiceMenu.SetActive(false);
		_patientEntryMenu.SetActive(false);
		_doctorEntryMenu.SetActive(false);
		_patientCreateMenu.SetActive(false);
		_doctorCreateMenu.SetActive(false);
		_patientChoiceMenu.SetActive(false);
		_doctorChoiceMenu.SetActive(false);
		_patientMenu.SetActive(false);
		_doctorMenu.SetActive(false);
		_adminMenu.SetActive(false);

		menuToActivate.SetActive(true);
	}

	public void OpenMainMenu()
	{
		ActivateMenu(_mainMenu);
	}

	public void OpenChoiceMenu()
	{
		ActivateMenu(_choiceMenu);
	}

	public void OpenPatientEntryMenu()
	{
		ActivateMenu(_patientEntryMenu);
	}

	public void OpenDoctorEntryMenu()
	{
		ActivateMenu(_doctorEntryMenu);
	}

	public void OpenPatientCreateMenu()
	{
		ActivateMenu(_patientCreateMenu);
	}

	public void OpenDoctorCreateMenu()
	{
		ActivateMenu(_doctorCreateMenu);
	}

	public void OpenPatientChoiceMenu()
	{
		ActivateMenu(_patientChoiceMenu);
	}

	public void OpenDoctorChoiceMenu()
	{
		ActivateMenu(_doctorChoiceMenu);
	}

	public void OpenPatientMenu()
	{
		ActivateMenu(_patientMenu);
	}

	public void OpenDoctorMenu()
	{
		ActivateMenu(_doctorMenu);
	}

	public void OpenAdminMenu()
	{
		ActivateMenu(_adminMenu);
	}
}
