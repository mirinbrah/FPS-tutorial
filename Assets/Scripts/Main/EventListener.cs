using UnityEngine;

public class EventListener : MonoBehaviour
{
    [SerializeField] private EventBlock[] blocksToListen; 

    private void OnEnable()
    {
        foreach (var block in blocksToListen)
        {
            if (block != null)
            {
                block.OnPlayerEnterCSharp += DothingForMe;
            }
        }
    }

    private void OnDisable()
    {
        foreach (var block in blocksToListen)
        {
            if (block != null)
            {
                block.OnPlayerEnterCSharp -= DothingForMe;
            }
        }
    }

    private void DothingForMe(string blockName)
    {
        Debug.Log($"<color=green>[C# LISTENER]</color> Кодом поймано событие от: {blockName}, я функция DothingForMe");
    }

    //private void HandleCSharpEvent(string blockName)
    //{
    //    Debug.Log($"<color=green>[C# LISTENER]</color> Кодом поймано событие от: {blockName}");
    //}

    public void HandleUnityEvent()
    {
        Debug.Log($"<color=orange>[UNITY LISTENER]</color> Инспектором поймано событие!");
    }
}