using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 12f;
    [SerializeField] private float sprintSpeed = 24f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float crouchSpeed = 6f;

    [Header("Physics")]
    [SerializeField] private float gravity = -9.81f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    private float originalHeight;
    private Vector3 originalCenter;
    private bool isCrouching;
    private bool isSprinting;

    private enum MovementState { Walking, Sprinting, Crouching }
    private MovementState currentState;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
        originalCenter = controller.center;
    }

    private void Start()
    {
        currentState = MovementState.Walking;
        Debug.Log($"Состояние: Иду, Скорость: {speed}");
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            float newHeight = originalHeight / 2f;
            controller.height = newHeight;

            float heightDifference = originalHeight - newHeight;
            controller.center = new Vector3(originalCenter.x, originalCenter.y - heightDifference / 2f, originalCenter.z);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            controller.height = originalHeight;
            controller.center = originalCenter;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.right * x + transform.forward * z;
        bool isTryingToMove = moveDirection.magnitude > 0.1f;

        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching && isTryingToMove)
        {
            if (isGrounded)
            {
                isSprinting = true;
            }
        }
        else
        {
            isSprinting = false;
        }

        float currentSpeed;
        MovementState newState;

        if (isCrouching)
        {
            currentSpeed = crouchSpeed;
            newState = MovementState.Crouching;
        }
        else if (isSprinting)
        {
            currentSpeed = sprintSpeed;
            newState = MovementState.Sprinting;
        }
        else
        {
            currentSpeed = speed;
            newState = MovementState.Walking;
        }

        if (newState != currentState)
        {
            currentState = newState;
            switch (currentState)
            {
                case MovementState.Walking:
                    Debug.Log($"Состояние: Иду, Скорость: {currentSpeed}");
                    break;
                case MovementState.Sprinting:
                    Debug.Log($"Состояние: Бегу, Скорость: {currentSpeed}");
                    break;
                case MovementState.Crouching:
                    Debug.Log($"Состояние: Крадусь, Скорость: {currentSpeed}");
                    break;
            }
        }

        Vector3 horizontalMove = moveDirection * currentSpeed;

        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;

        Vector3 finalMove = horizontalMove + playerVelocity;
        controller.Move(finalMove * Time.deltaTime);
    }
}