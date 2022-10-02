using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndIntroCutsceneScript : MonoBehaviour
{
    private void OnEnable()
    {
        GameStateController.Instance.CurrentGameState = GameStateController.GameState.ENTERING_GAME;
        SceneManager.LoadScene(Scenes.LOADING_SCREEN);
    }
}
