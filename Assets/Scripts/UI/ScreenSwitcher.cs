using System.Collections.Generic;
using UnityEngine;

public class ScreenSwitcher : MonoBehaviour
{
	[SerializeField] private GameObject _startMenu;

	private List<GameObject> _objectsToHide = new();

	public void SwitchObjects(GameObject objectToActivate)
	{
		foreach (var obj in _objectsToHide)
		{
			obj.SetActive(false);
		}

		objectToActivate.SetActive(true);
	}

	private void Start()
	{
		foreach (Transform obj in transform)
		{
			_objectsToHide.Add(obj.gameObject);
		}

		SwitchObjects(_startMenu);
	}
}
