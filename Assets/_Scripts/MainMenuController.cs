using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartNewGame()
    {
        GameStateController.Instance.CurrentGameState = GameStateController.GameState.ENTERING_GAME;
        SceneManager.LoadScene(Scenes.LOADING_SCREEN);
    }

}
