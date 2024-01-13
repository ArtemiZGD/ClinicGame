using System.Collections.Generic;
using UnityEngine;

public class HeartsController : MonoBehaviour
{
	[SerializeField] private List<Heart> _hearts;  // Список объектов "Сердце"

	public bool IsAlive => _hp > 0;  // Проверка, жив ли игрок

	private int _hp;  // Количество жизней

	// Получение урона
	public void TakeDamage()
	{
		if (_hp > 0)
		{
			_hp--;
			_hearts[_hp].SetActive(false);  // Деактивация соответствующего сердца
		}
		else
		{
			Debug.LogError("_hp < 0");  // Вывод ошибки, если жизней уже нет
		}
	}

	// Сброс количества жизней и активация всех сердец
	public void ResetHearts()
	{
		_hp = _hearts.Count;

		foreach (var heart in _hearts)
		{
			heart.SetActive(true);
		}
	}

	// Вызывается при старте объекта
	private void Start()
	{
		ResetHearts();  // Инициализация сердец при старте
	}
}
