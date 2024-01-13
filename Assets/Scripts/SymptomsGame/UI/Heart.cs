using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
	[SerializeField] private Image _heartIcon;  // ������ ������
	[SerializeField] private Color _activatedHeartColor;  // ���� ��������������� ������
	[SerializeField] private Color _deactivatedHeartColor;  // ���� ����������������� ������

	// ������������� ��������� ������ (������������/��������������)
	public void SetActive(bool isActive)
	{
		// ������������� ���� ������ � ����������� �� ���������
		_heartIcon.color = isActive ? _activatedHeartColor : _deactivatedHeartColor;
	}
}
