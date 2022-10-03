using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject _pressAnyButton;
    [SerializeField] GameObject _buttonContainer;
    [SerializeField] GameObject _settingsObject;
    [SerializeField] AudioClip _newGameSound;

    void Start()
    {
        GameStateController.Instance.CurrentGameState = GameStateController.GameState.MAIN_MENU;
        AudioManager.Instance.PlayMainMenuMusic();
        PlayerManager.Instance.OnNewPlayerJoined += OnPlayerCreated;
    }

    void OnDestroy() => PlayerManager.Instance.OnNewPlayerJoined -= OnPlayerCreated;

    private void OnPlayerCreated()
    {
        Debug.Log("hola");
        _pressAnyButton.SetActive(false);
        _buttonContainer.SetActive(true);
    }

    public void StartNewGame()
    {
        GameStateController.Instance.CurrentGameState = GameStateController.GameState.OPENING_CINEMATIC;
        AudioManager.Instance.Stop();
        //AudioManager.Instance.AudioSource.PlayOneShot(_newGameSound);

        // TODO: Fade screen to black while track is playing, and then load new level
        SceneManager.LoadScene(Scenes.LOADING_SCREEN);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LoadCredits()
    {
        AudioManager.Instance.Stop();
        SceneManager.LoadScene(Scenes.CREDITS);
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
