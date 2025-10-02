using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensetivity = 500f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    public float topClamp = -90f;
    public float bottomClamp = 90f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensetivity* Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
