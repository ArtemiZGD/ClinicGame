using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private Transform _contentParent;
    [SerializeField] private Message _patientMessagePrefab;
    [SerializeField] private Message _doctorMessagePrefab;

    public void AddPatientMessage(string message)
    {
        AddMessage(message, _patientMessagePrefab);
    }

    public void AddDoctorMessage(string message)
    {
        AddMessage(message, _doctorMessagePrefab);
    }

    public void ResetMessages()
    {
        List<Transform> childs = new();

        foreach (Transform child in _contentParent)
        {
            childs.Add(child);
        }

        for (int i = 0; i < childs.Count; i++)
        {
            Destroy(childs[i].gameObject);
        }
    }

    private void AddMessage(string message, Message messagePrefab)
    {
        Message newMessage = Instantiate(messagePrefab, _contentParent);
        newMessage.Type(message);
    }
}
