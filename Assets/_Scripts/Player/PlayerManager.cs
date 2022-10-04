using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    readonly int ANIM_DEAD_TRIGGERED = Animator.StringToHash("Death");
    readonly int ANIM_RESPAWN_TRIGGERED = Animator.StringToHash("Respawn");

    [SerializeField] PlayerInputManager _playerInputManager;

    // Just to get the timer, that's it
    [SerializeField] AnimationClip _deathAnimation;

    PlayerInput _playerInput;
    PlayerReferences _playerReferences;

    float _horizontalSensitivity;
    float _verticalSensitivity;

    public PlayerInput PlayerInput { get => _playerInput; }
    public PlayerReferences PlayerReferences { get => _playerReferences; }
    public Transform RespawnPoint;

    public float HorizontalSensitivity
    {
        get { return _horizontalSensitivity; }
        set
        {
            _horizontalSensitivity = value;
            if (_playerReferences)
            {
                _playerReferences.FreeLookCam.m_XAxis.m_MaxSpeed = _horizontalSensitivity * MAX_HORIZONTAL_SENSITIVITY;
                SaveHorizontalSensitivitySettings(value);
            }
        }
    }
    public float VerticalSensitivity
    {
        get { return _verticalSensitivity; }
        set
        {
            _verticalSensitivity = value;
            if (_playerReferences)
            {
                _playerReferences.FreeLookCam.m_YAxis.m_MaxSpeed = _verticalSensitivity * MAX_VERTICAL_SENSITIVITY;
                SaveVerticalSensitivitySettings(value);
            }
        }
    }

    // Cinemachine specific values
    public readonly float MAX_HORIZONTAL_SENSITIVITY = 250;
    public readonly float MAX_VERTICAL_SENSITIVITY = 2;
    readonly string SETTINGS_HORIZONTAL = "Horizontal_Sensitivity";
    readonly string SETTINGS_VERTICAL = "Vertical_Sensitivity";
    readonly float DEFAULT_HORIZONTAL_SLIDER_SENSITIVITY = 0.6f;
    readonly float DEFAULT_VERTICAL_SLIDER_SENSITIVITY = 0.75f;

    public static PlayerManager Instance { get; private set; }
    public event Action OnNewPlayerJoined;

    bool isPlayerDead;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);

        _playerInputManager.onPlayerJoined += OnPlayerCreated;
        LoadSensitivitySettings();
    }

    void OnDestroy() => _playerInputManager.onPlayerJoined -= OnPlayerCreated;


    private void OnPlayerCreated(PlayerInput playerInput)
    {
        OnNewPlayerJoined?.Invoke();
        DontDestroyOnLoad(playerInput.gameObject);
        _playerInput = playerInput;
        _playerInputManager.DisableJoining();
        _playerInputManager.onPlayerJoined -= OnPlayerCreated;

        _playerReferences = playerInput.GetComponent<PlayerReferences>();
        _playerReferences.PlayerInputController.IsShowingMainMenu = true;
        _playerReferences.MakePlayerKinematic();
        _playerReferences.DisableVisuals();
        _playerReferences.DisableCameras();
        _playerReferences.DisableControllerScript();
        _playerReferences.FreeLookCam.m_XAxis.m_MaxSpeed = HorizontalSensitivity * MAX_HORIZONTAL_SENSITIVITY;
        _playerReferences.FreeLookCam.m_YAxis.m_MaxSpeed = VerticalSensitivity * MAX_VERTICAL_SENSITIVITY;
        playerInput.SwitchCurrentActionMap("UI");
    }

    void LoadSensitivitySettings()
    {
        if (PlayerPrefs.HasKey(SETTINGS_HORIZONTAL))
        {
            float horizontal = PlayerPrefs.GetFloat(SETTINGS_HORIZONTAL);
            float vertical = PlayerPrefs.GetFloat(SETTINGS_VERTICAL);
            HorizontalSensitivity = horizontal;
            VerticalSensitivity = vertical;
            return;
        }

        HorizontalSensitivity = DEFAULT_HORIZONTAL_SLIDER_SENSITIVITY;
        VerticalSensitivity = DEFAULT_VERTICAL_SLIDER_SENSITIVITY;
    }
    private void SaveHorizontalSensitivitySettings(float sliderValue)
    {
        PlayerPrefs.SetFloat(SETTINGS_HORIZONTAL, sliderValue);
    }
    private void SaveVerticalSensitivitySettings(float sliderValue)
    {
        PlayerPrefs.SetFloat(SETTINGS_VERTICAL, sliderValue);
    }

    public void DisablePlayerInteraction()
    {
        _playerReferences.DisableControllerScript();
    }

    public void EnablePlayerInteraction()
    {
        _playerReferences.EnableControllerScript();
    }

    public void EnablePlayerInteractionAndCamera()
    {
        _playerReferences.EnableCameras();
        _playerReferences.EnableControllerScript();
    }

    public void DisablePlayerInteractionAndCamera()
    {
        _playerReferences.DisableCameras();
        _playerReferences.DisableControllerScript();
    }

    public void OnPlayerDeath()
    {
        if (isPlayerDead)
        {
            return;
        }
        isPlayerDead = true;
        DisablePlayerInteraction();
        PlayerReferences.PlayerInput.SwitchCurrentActionMap("UI");
        PlayerReferences.PlayerAnimator.ResetTrigger(ANIM_RESPAWN_TRIGGERED);
        PlayerReferences.PlayerAnimator.SetTrigger(ANIM_DEAD_TRIGGERED);

        StartCoroutine(WaitForDeathAnimation(_deathAnimation.length + 1));
    }

    public void RespawnPlayer()
    {
        PlayerInput.gameObject.transform.SetPositionAndRotation(
                    RespawnPoint.position, RespawnPoint.rotation);
        isPlayerDead = false;
        EnablePlayerInteraction();
        PlayerReferences.PlayerAnimator.ResetTrigger(ANIM_DEAD_TRIGGERED);
        PlayerReferences.PlayerAnimator.SetTrigger(ANIM_RESPAWN_TRIGGERED);
        PlayerReferences.PlayerInput.SwitchCurrentActionMap("Player");

        OnPlayerRespawned?.Invoke();
    }
    public static event Action OnPlayerRespawned;

    public void ClearPlayer()
    {
        if (_playerReferences != null)
        {
            Destroy(_playerReferences.gameObject);
            _playerInput = null;
            _playerReferences = null;
        }

        RespawnPoint = null;
        _playerInputManager.onPlayerJoined += OnPlayerCreated;
        _playerInputManager.EnableJoining();
    }

    IEnumerator WaitForDeathAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        PlayerReferences.PlayerInputController.OnDeath();
    }
}
