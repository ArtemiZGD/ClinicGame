using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
	[SerializeField] private Image _heartIcon;  // Иконка сердца
	[SerializeField] private Color _activatedHeartColor;  // Цвет активированного сердца
	[SerializeField] private Color _deactivatedHeartColor;  // Цвет деактивированного сердца

	// Устанавливает состояние сердца (активировано/деактивировано)
	public void SetActive(bool isActive)
	{
		// Устанавливаем цвет иконки в зависимости от состояния
		_heartIcon.color = isActive ? _activatedHeartColor : _deactivatedHeartColor;
	}
}
