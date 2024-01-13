using UnityEngine;

public class ExitButton : MonoBehaviour
{
	// Выход из игры
	public void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}
