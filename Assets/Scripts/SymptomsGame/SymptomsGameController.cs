using UnityEngine;

public class SymptomsGameController : MonoBehaviour
{
    [SerializeField] private PatientGenerator _patientGenerator;
    [SerializeField] private HeartsController _heartsController;
    [Header("UI")]
    [SerializeField] private ScreenSwitcher _screenSwitcher;
    [SerializeField] private GameObject _gameOverMenu;

    public void CheckAnswer(DiseaseData disease)
    {
        if (disease.Name == _patientGenerator.Patient.Disease.Name)
        {
            Debug.Log("Correct");
            RegeneratePatient();
        }
        else
        {
            _heartsController.TakeDamage();
            RegeneratePatient();
        }

        if (_heartsController.IsAlive == false)
        {
            RegeneratePatient();
            _heartsController.ResetHearts();
            _screenSwitcher.SwitchScreen(_gameOverMenu);
        }
    }

    private void RegeneratePatient()
    {
        _patientGenerator.GeneratePatient();
    }
}
