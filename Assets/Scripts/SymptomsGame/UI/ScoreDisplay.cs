using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
	[SerializeField] private SymptomsGameController _gameController;
	[SerializeField] private TMP_Text _scoreText;
	[SerializeField] private string _beginningText;

	private void OnEnable()
	{
		_scoreText.text = _beginningText + _gameController.Score;
	}
}
