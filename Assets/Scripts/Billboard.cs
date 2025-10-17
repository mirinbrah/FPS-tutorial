using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class Billboard : MonoBehaviour
{
    private Camera mainCamera;
    private Canvas canvas;

    void Start()
    {
        mainCamera = Camera.main;
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = mainCamera;
    }

    void LateUpdate()
    {
        transform.rotation = mainCamera.transform.rotation;
    }
}