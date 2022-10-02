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
    [SerializeField] CapsuleCollider _standingCollider;
    [SerializeField] CapsuleCollider _frictionlessStandingCollider;
    [SerializeField] CapsuleCollider _crouchingCollider;
    [SerializeField] CapsuleCollider _frictionlessCrouchingCollider;
    CapsuleCollider _currentCollider;
    [Header("Movement Settings")]
    [SerializeField] float _walkSpeed;
    [SerializeField] float _runSpeed;
    [SerializeField] float _jumpForce;
    [SerializeField] float _playerRotationSpeed;
    [Header("Ground Checking Settings")]
    [SerializeField] LayerMask _checkMasks;
    [SerializeField] float _groundCheckRadius;
    [SerializeField] float _groundCheckDistance;
    [SerializeField] float _slopeForce;
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
    bool _isInAir;
    bool _isCrouching;

    readonly int ANIM_VEL_Z = Animator.StringToHash("velocityZ");
    readonly int ANIM_VEL_Y = Animator.StringToHash("velocityY");
    readonly int ANIM_IS_RUNNING = Animator.StringToHash("isRunning");
    readonly int ANIM_IS_CROUCHING = Animator.StringToHash("isCrouching");
    readonly int ANIM_IS_GROUNDED = Animator.StringToHash("isGrounded");
    readonly int ANIM_JUMP_PRESSED = Animator.StringToHash("jumpPressed");

    public bool IsMoving { get => _moveInput != Vector2.zero; }

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _currentCollider = _standingCollider;
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
        GroundCheckBySphereCast();
    }

    void GroundCheckBySphereCast()
    {
        if (Physics.SphereCast(
            transform.position + Vector3.up * (_currentCollider.radius + Physics.defaultContactOffset),
            _currentCollider.radius - Physics.defaultContactOffset,
            Vector3.down,
            out RaycastHit hitInfo,
            _groundCheckDistance,
            _checkMasks,
            QueryTriggerInteraction.Ignore))
        {
            _isGrounded = true;
            _playerAnimator.SetBool(ANIM_IS_GROUNDED, true);

            if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f)
            {
                _rigidBody.velocity = Vector3.ProjectOnPlane(_rigidBody.velocity, hitInfo.normal);
                _rigidBody.AddForce(Vector3.down * _slopeForce, ForceMode.Force);
            }
        }
        else
        {
            _isGrounded = false;
            _playerAnimator.SetBool(ANIM_IS_GROUNDED, false);
        }
    }

    void GroundCheckByCheckSphere()
    {
        if (Physics.CheckSphere(_groundChecker.position, _groundCheckRadius, _checkMasks))
        {
            _isGrounded = true;
            _playerAnimator.SetBool(ANIM_IS_GROUNDED, true);
        }
        else
        {
            _isGrounded = false;
            _playerAnimator.SetBool(ANIM_IS_GROUNDED, false);
        }
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
        if (_isGrounded && !_isCrouching)
        {
            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

            _playerAnimator.SetTrigger(ANIM_JUMP_PRESSED);
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.DAMAGER))
        {
            PlayerManager.Instance.OnPlayerDeath();
        }
    }

    #region ANIMATIONS
    void UpdateAnimationVelocity()
    {
        if (!_isGrounded)
            _playerAnimator.SetFloat(ANIM_VEL_Y, _rigidBody.velocity.y);
        else
            _playerAnimator.SetFloat(ANIM_VEL_Y, 0.0f);

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

        if (_isCrouching)
        {
            _standingCollider.enabled = false;
            _frictionlessStandingCollider.enabled = false;
            _crouchingCollider.enabled = true;
            _frictionlessCrouchingCollider.enabled = true;

            _currentCollider = _crouchingCollider;
        }
        else
        {
            _crouchingCollider.enabled = false;
            _frictionlessCrouchingCollider.enabled = false;
            _standingCollider.enabled = true;
            _frictionlessStandingCollider.enabled = true;

            _currentCollider = _standingCollider;
        }
    }
    #endregion
}
