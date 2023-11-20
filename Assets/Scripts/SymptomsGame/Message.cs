using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    [SerializeField] private TMP_Text _messageText;

    public void Type(string text)
    {
        _messageText.text = text;
    }
}
