using UnityEngine;
using UnityEngine.Events; 
using System;             

public class EventBlock : MonoBehaviour
{
    [Header("1. Unity Event (Настройка в Инспекторе)")]

    public UnityEvent OnPlayerEnterUnity;


    public event Action<string> OnPlayerEnterCSharp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"<color=cyan>[BLOCK]</color> Игрок наступил на {gameObject.name}");

            OnPlayerEnterUnity?.Invoke();

            OnPlayerEnterCSharp?.Invoke(gameObject.name);
        }
    }

    public void PrintDebugMessage(string message)
    {
        Debug.Log(message);
    }
}