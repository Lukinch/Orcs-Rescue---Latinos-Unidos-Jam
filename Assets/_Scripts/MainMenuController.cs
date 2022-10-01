using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] PlayerInputManager _playerInputManager;
    [SerializeField] GameObject _pressAnyButton;
    [SerializeField] GameObject _buttonContainer;
    [SerializeField] GameObject _settingsObject;

    void Awake() => _playerInputManager.onPlayerJoined += OnPlayerCreated;

    void OnDestroy() => _playerInputManager.onPlayerJoined -= OnPlayerCreated;

    private void OnPlayerCreated(PlayerInput playerInput)
    {
        _playerInputManager.onPlayerJoined -= OnPlayerCreated;
        _pressAnyButton.SetActive(false);
        _buttonContainer.SetActive(true);
    }

    public void StartNewGame()
    {
        GameStateController.Instance.CurrentGameState = GameStateController.GameState.ENTERING_GAME;
        SceneManager.LoadScene(Scenes.LOADING_SCREEN);
        PlayerManager.Instance.PlayerReferences.EnableVisuals();
        PlayerManager.Instance.PlayerReferences.EnableCameras();
        PlayerManager.Instance.PlayerReferences.EnableControllerScript();
        PlayerManager.Instance.PlayerReferences.MakePlayerDynamic();
        PlayerManager.Instance.PlayerInput.SwitchCurrentActionMap("Player");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
