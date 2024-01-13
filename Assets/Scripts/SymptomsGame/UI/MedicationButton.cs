using TMPro;
using UnityEngine;

public class MedicationButton : MonoBehaviour
{
	[SerializeField] private TMP_Text _text;  // Текстовое поле для отображения имени лекарства

	private MedicationData _medication;  // Данные о лекарстве
	private SymptomsGameController _gameController;  // Контроллер игры с симптомами

	// Инициализация кнопки лекарства
	public void Init(SymptomsGameController gameController, MedicationData medication)
	{
		_gameController = gameController;
		_medication = medication;
		_text.text = medication.Name;  // Устанавливаем имя лекарства в текстовое поле
	}

	// Проверка ответа на лекарство
	public void CheckAnswer()
	{
		_gameController.CheckMedicationAnswer(_medication);  // Вызываем метод контроллера для проверки ответа на лекарство
	}
}
