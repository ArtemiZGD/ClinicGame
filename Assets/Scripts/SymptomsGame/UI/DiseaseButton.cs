using TMPro;
using UnityEngine;

public class DiseaseButton : MonoBehaviour
{
	[SerializeField] private TMP_Text _text;  // Текстовое поле для отображения имени болезни

	private DiseaseData _disease;  // Данные о болезни
	private SymptomsGameController _gameController;  // Контроллер игры с симптомами

	// Инициализация кнопки болезни
	public void Init(SymptomsGameController gameController, DiseaseData disease)
	{
		_gameController = gameController;
		_disease = disease;
		_text.text = disease.Name;  // Устанавливаем имя болезни в текстовое поле
	}

	// Проверка ответа на болезнь
	public void CheckAnswer()
	{
		_gameController.CheckDiseaseAnswer(_disease);  // Вызываем метод контроллера для проверки ответа на болезнь
	}
}
