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

    public PlayerInput PlayerInput { get => _playerInput; }
    public PlayerReferences PlayerReferences { get => _playerReferences; }

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
        _playerReferences.MakePlayerKinematic();
        _playerReferences.DisableVisuals();
        _playerReferences.DisableCameras();
        _playerReferences.DisableControllerScript();
        playerInput.SwitchCurrentActionMap("UI");
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
        PlayerReferences.PlayerAnimator.ResetTrigger(ANIM_RESPAWN_TRIGGERED);
        PlayerReferences.PlayerAnimator.SetTrigger(ANIM_DEAD_TRIGGERED);

        StartCoroutine(WaitForDeathAnimation(_deathAnimation.length + 1));
    }

    public void RespawnPlayer()
    {
        isPlayerDead = false;
        EnablePlayerInteraction();
        PlayerReferences.PlayerAnimator.ResetTrigger(ANIM_DEAD_TRIGGERED);
        PlayerReferences.PlayerAnimator.SetTrigger(ANIM_RESPAWN_TRIGGERED);
    }

    public void ClearPlayer()
    {
        _playerInput = null;
        _playerReferences = null;

        _playerInputManager.onPlayerJoined += OnPlayerCreated;
        _playerInputManager.EnableJoining();
    }

    IEnumerator WaitForDeathAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        PlayerReferences.PlayerInputController.OnDeath();
    }
}
