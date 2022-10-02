using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _deathMenu;

    public bool IsShowingDeathScreen { get; set; }

    public void OnDeath()
    {
        if (IsShowingDeathScreen) return;

        Time.timeScale = 0;
        _playerInput.SwitchCurrentActionMap("UI");
        _deathMenu.SetActive(true);
        IsShowingDeathScreen = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.Instance.Pause();
    }

    public void OnPause()
    {
        if (IsShowingDeathScreen) return;

        Time.timeScale = 0;
        _playerInput.SwitchCurrentActionMap("UI");
        _pauseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.Instance.Pause();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (IsShowingDeathScreen) return;

        Time.timeScale = 0;
        _playerInput.SwitchCurrentActionMap("UI");
        _pauseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.Instance.Pause();
    }

    public void OnResume()
    {
        if (IsShowingDeathScreen) return;

        _pauseMenu.SetActive(false);
        _playerInput.SwitchCurrentActionMap("Player");
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioManager.Instance.Resume();
    }

    public void OnResume(InputAction.CallbackContext context)
    {
        if (IsShowingDeathScreen) return;

        _pauseMenu.SetActive(false);
        _playerInput.SwitchCurrentActionMap("Player");
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioManager.Instance.Resume();
    }

    public void ContinueGame()
    {
        _deathMenu.SetActive(false);
        _playerInput.SwitchCurrentActionMap("Player");
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioManager.Instance.Resume();
        IsShowingDeathScreen = false;
        PlayerManager.Instance.RespawnPlayer();
    }

    public void ExitGame()
    {
        PlayerManager.Instance.PlayerReferences.PlayerInput.SwitchCurrentActionMap("UI");

        if (IsShowingDeathScreen) _deathMenu.SetActive(false);
        else _pauseMenu.SetActive(false);

        Time.timeScale = 1;
        AudioManager.Instance.Resume();
        PlayerManager.Instance.ClearPlayer();
        SceneManager.LoadScene(Scenes.MAIN_MENU);
        Destroy(gameObject);
    }
}
