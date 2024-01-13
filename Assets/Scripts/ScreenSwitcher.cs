using System.Collections.Generic;
using UnityEngine;

public class ScreenSwitcher : MonoBehaviour
{
	[SerializeField] private GameObject _startScreen;  // Начальный экран

	private readonly List<GameObject> _objectsToHide = new List<GameObject>();  // Список объектов для скрытия

	private void Start()
	{
		// Заполняем список объектов для скрытия при старте
		foreach (Transform obj in transform)
		{
			_objectsToHide.Add(obj.gameObject);
		}

		// Переключаемся на начальный экран
		SwitchScreen(_startScreen);
	}

	// Метод для переключения экрана
	public void SwitchScreen(GameObject objectToActivate)
	{
		// Скрываем все объекты
		foreach (var obj in _objectsToHide)
		{
			obj.SetActive(false);
		}

		// Активируем выбранный объект
		objectToActivate.SetActive(true);
	}
}
