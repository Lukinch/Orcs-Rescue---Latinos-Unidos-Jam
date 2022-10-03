using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            OnCreditsEnded();
        }
    }

    void OnCreditsEnded()
    {
        GameStateController.Instance.CurrentGameState = GameStateController.GameState.MAIN_MENU;
        SceneManager.LoadScene(Scenes.LOADING_SCREEN);
    }
}
