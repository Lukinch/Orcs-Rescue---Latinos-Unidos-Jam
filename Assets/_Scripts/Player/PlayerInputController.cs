using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        AudioManager.Instance.Pause();
    }

    public void OnResume()
    {
        _pauseMenu.SetActive(false);
        _playerInput.SwitchCurrentActionMap("Player");
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioManager.Instance.Resume();
    }

    public void OnResume(InputAction.CallbackContext context)
    {
        _pauseMenu.SetActive(false);
        _playerInput.SwitchCurrentActionMap("Player");
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioManager.Instance.Resume();
    }

    public void ExitGame()
    {
        PlayerManager.Instance.PlayerReferences.PlayerInput.SwitchCurrentActionMap("UI");
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        AudioManager.Instance.Resume();
        SceneManager.LoadScene(Scenes.MAIN_MENU);
    }
}
