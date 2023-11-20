using System.Collections.Generic;
using UnityEngine;

public class ScreenSwitcher : MonoBehaviour
{
	[SerializeField] private GameObject _startScreen;

	private readonly List<GameObject> _objectsToHide = new();

	private void Start()
	{
		foreach (Transform obj in transform)
		{
			_objectsToHide.Add(obj.gameObject);
		}

		SwitchScreen(_startScreen);
	}

	public void SwitchScreen(GameObject objectToActivate)
	{
		foreach (var obj in _objectsToHide)
		{
			obj.SetActive(false);
		}

		objectToActivate.SetActive(true);
	}
}
