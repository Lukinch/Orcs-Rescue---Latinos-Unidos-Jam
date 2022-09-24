using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] Animator _playerAnimator;
    [SerializeField] Animator _cinemachineAnimator;
    [SerializeField] Transform _playerVisuals;
    [SerializeField] Transform _groundChecker;
    [SerializeField] Transform _camera;
    [Header("Movement Settings")]
    [SerializeField] float _walkSpeed;
    [SerializeField] float _runSpeed;
    [SerializeField] float _jumpForce;
    [SerializeField] float _playerRotationSpeed;
    [Header("Ground Checking Settings")]
    [SerializeField] float _groundCheckRadius;
    [Header("Animation Settings")]
    [SerializeField] float _animationChangeRate;
    [SerializeField] float _animationMaxWalkSpeed;
    [SerializeField] float _animationMaxRunSpeed;

    Rigidbody _rigidBody;
    Vector3 _moveDirection;
    Vector3 _transformFaceDirection;
    Vector3 _visualsForwardDirection;
    Vector2 _moveInput;
    Vector2 _lookInput;
    float _currentMoveSpeed;
    float _animMaxVelocity;
    float _animCurrentVelocity;
    bool _isRunPressed;
    bool _isJumpPressed;
    bool _isGrounded;
    bool _isCrouching;

    readonly int ANIM_VEL_Z = Animator.StringToHash("velocityZ");
    readonly int ANIM_IS_RUNNING = Animator.StringToHash("isRunning");
    readonly int ANIM_IS_CROUCHING = Animator.StringToHash("isCrouching");

    public bool IsMoving { get => _moveInput != Vector2.zero; }

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        UpdateAnimationVelocity();
        HandleRotations();
    }

    void FixedUpdate()
    {
        GroundCheck();
        Move();
    }

    void GroundCheck()
    {
        // TODO: IMPROVE THIS SHIT
        if (Physics.SphereCast(_groundChecker.position, _groundCheckRadius, Vector3.down, out RaycastHit hitInfo))
            _isGrounded = false;
        else
            _isGrounded = true;
    }

    void Move()
    {
        if (IsMoving)
        {
            if (_isCrouching)
                _currentMoveSpeed = _walkSpeed;
            else
                _currentMoveSpeed = _isRunPressed ? _runSpeed : _walkSpeed;
        }
        else
            _currentMoveSpeed = 0.0f;

        // Get where the player object is facing
        // Multiply it by the moveInput to get the final direction and amount to where the velocity should be facing
        _moveDirection = (transform.right * _moveInput.x + transform.forward * _moveInput.y).normalized;
        _moveDirection *= _currentMoveSpeed;
        _moveDirection.y = _rigidBody.velocity.y;

        // Apply the velocity to the rb in world space
        _rigidBody.velocity = _moveDirection;
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    void HandleRotations()
    {
        if (!IsMoving) return;

        RotateTransformWithCamera();
        RotateVisuals();
    }

    void RotateTransformWithCamera()
    {
        _transformFaceDirection = _camera.forward;
        _transformFaceDirection.y = 0.0f;
        transform.forward = _transformFaceDirection;
    }

    void RotateVisuals()
    {
        // Get the moveInput Direction (already normalized)
        // Get the camera normalized x and z
        // Sum both vectors to get the final looking vector
        _visualsForwardDirection = (transform.right * _moveInput.x + transform.forward * _moveInput.y).normalized;

        // Spherical interpolate the visuals ONLY towards the normalizedDirection
        _playerVisuals.forward = Vector3.Slerp(_playerVisuals.forward, _visualsForwardDirection, Time.deltaTime * _playerRotationSpeed);
    }

    #region ANIMATIONS
    void UpdateAnimationVelocity()
    {
        if (IsMoving)
            _animMaxVelocity = _isRunPressed ? _animationMaxRunSpeed : _animationMaxWalkSpeed;
        else
            _animMaxVelocity = 0.0f;

        _animCurrentVelocity = Mathf.MoveTowards(_animCurrentVelocity, _animMaxVelocity, Time.deltaTime * _animationChangeRate);
        _playerAnimator.SetFloat(ANIM_VEL_Z, _animCurrentVelocity);
    }
    #endregion

    #region INPUT METHODS
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Jump();
        }
        // if (context.performed) _isJumpPressed = true;
        // else if (context.canceled) _isJumpPressed = false;
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed) _isRunPressed = true;
        else if (context.canceled) _isRunPressed = false;
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed) _isCrouching = !_isCrouching;

        _playerAnimator.SetBool(ANIM_IS_CROUCHING, _isCrouching);
    }
    #endregion

















    // void ProcessMovement()
    // {
    //     Vector3 movementVector = new Vector3();
    //     // Forward & Backward movement
    //     movementVector += transform.forward * movementInput.y;
    //     // Left & Right movement
    //     movementVector += transform.right * movementInput.x;
    //     // Normalize direction vector, so the speed is not higher when moving in diagonal
    //     movementVector.Normalize();
    //     // Modify the speed (*2) if Player is running
    //     float speed = walkSpeed * (isCrouching ? crouchSpeedCoefficient : 1);
    //     movementVector *= speed * (canRun ? 2 : 1) * Time.deltaTime;
    //     // Do not modify the velocity in Y axis
    //     movementVector.y = rigidbody.velocity.y;
    //     // Change the velocity of the RigidBody to apply movement
    //     rigidbody.velocity = movementVector;
    // }

    // void StickToGround()
    // {
    //     RaycastHit hitInfo;
    //     CapsuleCollider collider = isCrouching ? ((CapsuleCollider)crouchingCollider) : ((CapsuleCollider)standingCollider);

    //     if (Physics.SphereCast(
    //         transform.position,
    //         collider.radius * (1.0f - stickToGroundOffset),
    //         Vector3.down,
    //         out hitInfo,
    //         ((collider.height / 2f) - collider.radius) + stickToGroundDistance,
    //         Physics.AllLayers,
    //         QueryTriggerInteraction.Ignore))
    //     {
    //         if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f)
    //         {
    //             rigidbody.velocity = Vector3.ProjectOnPlane(rigidbody.velocity, hitInfo.normal);
    //         }
    //     }
    // }
}
