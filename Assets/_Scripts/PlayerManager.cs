using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerInputManager _playerInputManager;

    PlayerInput _playerInput;
    PlayerReferences _playerReferences;

    public PlayerInput PlayerInput { get => _playerInput; }
    public PlayerReferences PlayerReferences { get => _playerReferences; }

    public static PlayerManager Instance { get; private set; }


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
}
