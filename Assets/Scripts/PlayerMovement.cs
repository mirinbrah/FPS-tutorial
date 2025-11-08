using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerCamera;

    [Header("Movement")]
    public float speed = 12f;
    public float sprintSpeed = 18f;
    public float crouchSpeed = 6f;

    [Header("Jumping & Gravity")]
    public float jumpHeight = 3f;
    public float gravity = -20f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;


    [Header("Animator")]
    //[SerializeField] private Animator armsAnimator;

    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;

    private CharacterController _controller;
    private Vector3 _playerVelocity; 
    private float _currentSpeed;
    private float _originalHeight;
    private Vector3 _originalCenter;
    private Vector3 _cameraOriginalPos;

    public bool IsGrounded { get; private set; }
    public bool IsMovementPressed { get; private set; }
    public bool IsSprintPressed { get; private set; }
    public bool IsCrouchPressed { get; private set; }
    public bool IsJumpPressed { get; private set; }

    public PlayerBaseState CurrentState { get => _currentState; set => _currentState = value; }

    //public Animator ArmsAnimator { get => armsAnimator; }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _originalHeight = _controller.height;
        _originalCenter = _controller.center;
        if (playerCamera != null)
        {
            _cameraOriginalPos = playerCamera.localPosition;
        }

        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }

    void Update()
    {
        HandleInput();
        CheckGrounded();

        _currentState.UpdateState();

        ApplyGravity();
        MovePlayer();
    }

    private void HandleInput()
    {
        Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        IsMovementPressed = movementInput.magnitude > 0.1f;
        IsSprintPressed = Input.GetKey(KeyCode.LeftShift);
        IsCrouchPressed = Input.GetKey(KeyCode.LeftControl);
        IsJumpPressed = Input.GetButtonDown("Jump");
    }

    private void CheckGrounded()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void ApplyGravity()
    {
        if (IsGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }
        _playerVelocity.y += gravity * Time.deltaTime;
    }

    private void MovePlayer()
    {
        Vector3 moveDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        Vector3 horizontalMove = moveDirection.normalized * _currentSpeed;
        Vector3 finalMove = horizontalMove + _playerVelocity;
        _controller.Move(finalMove * Time.deltaTime);
    }

    public void SetCurrentSpeed(float speed)
    {
        _currentSpeed = speed;
    }

    public void PerformJump()
    {
        _playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public void ApplyCrouch()
    {
        _controller.height = _originalHeight / 2f;
        float heightDifference = _originalHeight - _controller.height;
        _controller.center = new Vector3(_originalCenter.x, _originalCenter.y - heightDifference / 2f, _originalCenter.z);
        if (playerCamera != null)
        {
            playerCamera.localPosition = new Vector3(_cameraOriginalPos.x, _cameraOriginalPos.y - heightDifference / 2f, _cameraOriginalPos.z);
        }
    }

    public void StandUp()
    {
        _controller.height = _originalHeight;
        _controller.center = _originalCenter;
        if (playerCamera != null)
        {
            playerCamera.localPosition = _cameraOriginalPos;
        }
    }
}