using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] GameObject _pauseMenu;

    public void OnPause(InputAction.CallbackContext context)
    {
        Time.timeScale = 0;
        _playerInput.SwitchCurrentActionMap("UI");
        _pauseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnResume()
    {
        _pauseMenu.SetActive(false);
        _playerInput.SwitchCurrentActionMap("Player");
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnResume(InputAction.CallbackContext context)
    {
        _pauseMenu.SetActive(false);
        _playerInput.SwitchCurrentActionMap("Player");
        Time.timeScale = 1;

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
