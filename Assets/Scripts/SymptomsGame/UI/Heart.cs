using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    [SerializeField] private Image _heartIcon;
    [SerializeField] private Color _activatedHeartColor;
    [SerializeField] private Color _disactivatedHeartColor;

    public void SetActive(bool isActive)
    {
        if (isActive)
        {
            _heartIcon.color = _activatedHeartColor;
        }
        else
        {
            _heartIcon.color = _disactivatedHeartColor;
        }
    }
}
