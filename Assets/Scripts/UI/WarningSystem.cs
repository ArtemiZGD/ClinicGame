using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class WarningSystem : MonoBehaviour
{
	public static WarningSystem Instance { get; private set; } // ������ �� ������������ ��������� ������, ��������� ����� ����������� ����.

	[SerializeField] private TMP_Text _warning; // ������ �� ��������� TMP_Text ��� ����������� �������������� � ���������������� ����������.
	[SerializeField] private float _warningTime; // �����, � ������� �������� �������������� ����� ������������.

	private readonly StringBuilder _warningTextBuilder = new(); // ������ StringBuilder ��� ���������� ������ ��������������.

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this; // ������������� ������ �� ������������ ��������� ����� ������.
		}
		else
		{
			Destroy(gameObject); // ���� ��������� ��� ����������, ���������� ���� ������.
		}

		UpdateText(); // ��������� ����� � ���������� TMP_Text.
	}

	public void Warning(string text)
	{
		StartCoroutine(WarningCoroutine(text)); // �������� �������� ��� ����������� ��������������.
	}

	private IEnumerator WarningCoroutine(string text)
	{
		_warningTextBuilder.Insert(0, text); // ��������� ����� �������������� � ������ �������� ������.

		UpdateText(); // ��������� ����� � ���������� TMP_Text.

		yield return new WaitForSeconds(_warningTime); // ���� ��������� �����, ������ ��� ������ ��������������.

		_warningTextBuilder.Remove(_warningTextBuilder.Length - text.Length, text.Length); // ������� ����� �������������� �� �������� ������.

		UpdateText(); // ��������� ����� � ���������� TMP_Text.
	}

	private void UpdateText()
	{
		_warning.text = _warningTextBuilder.ToString(); // ������������� ����� �� ������� StringBuilder � ��������� TMP_Text ��� ����������� � ���������������� ����������.
	}
}
