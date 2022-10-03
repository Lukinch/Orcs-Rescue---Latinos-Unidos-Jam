using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    void Awake()
    {
        switch (GameStateController.Instance.CurrentGameState)
        {
            case GameStateController.GameState.OPENING_CINEMATIC:
                StartCoroutine(LoadOpeningCinematicScene());
                break;
            case GameStateController.GameState.ENTERING_GAME:
                StartCoroutine(LoadGameScenes());
                break;
            case GameStateController.GameState.ENDING_CINEMATIC:
                StartCoroutine(LoadEndingCinematicScene());
                break;
            case GameStateController.GameState.MAIN_MENU:
                LoadMainMenu();
                break;
            case GameStateController.GameState.GAME_COMPLETED:
                LoadCreditsScene();
                break;
        }
    }

    IEnumerator LoadOpeningCinematicScene()
    {
        AsyncOperation openingCinematicOperation = SceneManager.LoadSceneAsync(Scenes.OPENING_CINEMATIC_SCENE);
        openingCinematicOperation.allowSceneActivation = false;
        while (openingCinematicOperation.progress < 0.9)
        {
            yield return new WaitForEndOfFrame();
        }

        openingCinematicOperation.allowSceneActivation = true;
        SceneManager.UnloadSceneAsync(Scenes.LOADING_SCREEN);
    }

    IEnumerator LoadGameScenes()
    {
        AsyncOperation dungeonHallOperation = SceneManager.LoadSceneAsync(Scenes.DUNGEON_MAIN_HALL, LoadSceneMode.Additive);
        dungeonHallOperation.allowSceneActivation = false;
        AsyncOperation leftWingConnectorOperation = SceneManager.LoadSceneAsync(Scenes.DUNGEON_LEFT_WING_CONNECTOR, LoadSceneMode.Additive);
        leftWingConnectorOperation.allowSceneActivation = false;
        while (dungeonHallOperation.progress < 0.9 || leftWingConnectorOperation.progress < 0.9)
        {
            yield return new WaitForEndOfFrame();
        }

        dungeonHallOperation.allowSceneActivation = true;
        leftWingConnectorOperation.allowSceneActivation = true;
        SceneManager.UnloadSceneAsync(Scenes.LOADING_SCREEN);
    }

    IEnumerator LoadEndingCinematicScene()
    {
        if (PlayerManager.Instance.PlayerReferences) PlayerManager.Instance.ClearPlayer();
        AudioManager.Instance.Stop();
        AsyncOperation endingCinematicOperation = SceneManager.LoadSceneAsync(Scenes.ENDING_CINEMATIC_SCENE);
        endingCinematicOperation.allowSceneActivation = false;
        while (endingCinematicOperation.progress < 0.9)
        {
            yield return new WaitForEndOfFrame();
        }

        endingCinematicOperation.allowSceneActivation = true;
        SceneManager.UnloadSceneAsync(Scenes.LOADING_SCREEN);
    }

    void LoadMainMenu()
    {
        if (PlayerManager.Instance.PlayerReferences) PlayerManager.Instance.ClearPlayer();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(Scenes.MAIN_MENU);
    }

    void LoadCreditsScene()
    {
        SceneManager.LoadScene(Scenes.CREDITS);
    }
}
