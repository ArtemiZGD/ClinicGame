using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiseaseButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _button;

    private DiseaseData _disease;
    private SymptomsGameController _gameController;

    public void Init(SymptomsGameController gameController, DiseaseData disease)
    {
        _gameController = gameController;
        _disease = disease;
        _text.text = disease.Name;
    }

    public void CheckAnswer()
    {
        _gameController.CheckAnswer(_disease);
    }
}
