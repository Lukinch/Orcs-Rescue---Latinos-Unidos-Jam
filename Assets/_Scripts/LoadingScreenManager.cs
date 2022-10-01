using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    void Awake()
    {
        switch(GameStateController.Instance.CurrentGameState)
        {
            case GameStateController.GameState.OPENING_CINEMATIC:
                LoadOpeningCinematicScene();
                break;
            case GameStateController.GameState.ENTERING_GAME:
                StartCoroutine(LoadGameScenes());
                break;
            case GameStateController.GameState.ENDING_CINEMATIC:
                LoadEndingCinematicScene();
                break;
        }
    }

    void LoadOpeningCinematicScene()
    {

    }

    IEnumerator LoadGameScenes()
    {
        AsyncOperation dungeonHallOperation = SceneManager.LoadSceneAsync(Scenes.DUNGEON_MAIN_HALL, LoadSceneMode.Additive);
        dungeonHallOperation.allowSceneActivation = false;
        AsyncOperation leftWingConnectorOperation = SceneManager.LoadSceneAsync(Scenes.DUNGEON_LEFT_WING_CONNECTOR, LoadSceneMode.Additive);
        leftWingConnectorOperation.allowSceneActivation = false;
        while(dungeonHallOperation.progress < 0.9 || leftWingConnectorOperation.progress < 0.9)
        {
            yield return new WaitForEndOfFrame();
        }

        dungeonHallOperation.allowSceneActivation = true;
        leftWingConnectorOperation.allowSceneActivation = true;
        SceneManager.UnloadSceneAsync(Scenes.LOADING_SCREEN);
    }

    void LoadEndingCinematicScene()
    {

    }
}
