using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
	[SerializeField] private SymptomsGameController _gameController;  // Контроллер игры с симптомами
	[SerializeField] private TMP_Text _scoreText;  // Текстовое поле для отображения счета
	[SerializeField] private string _beginningText;  // Начальный текст

	// Вызывается при включении объекта
	private void OnEnable()
	{
		_scoreText.text = $"{_beginningText}{_gameController.Score}/{_gameController.MaxScore}";  // Обновление текста счета
	}
}
