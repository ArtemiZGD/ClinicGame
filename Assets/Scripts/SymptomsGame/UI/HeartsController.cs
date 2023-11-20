using System.Collections.Generic;
using UnityEngine;

public class HeartsController : MonoBehaviour
{
    [SerializeField] private List<Heart> _hearts;

    public bool IsAlive => _hp > 0;

    private int _hp;

    public void TakeDamage()
    {
        if (_hp > 0)
        {
            _hp--;
            _hearts[_hp].SetActive(false);
        }
        else
        {
            Debug.LogError("_hp < 0");
        }
    }

    public void ResetHearts()
    {
        _hp = _hearts.Count;

        foreach (var heart in _hearts)
        {
            heart.SetActive(true);
        }
    }

    private void Start()
    {
        ResetHearts();
    }
}
