using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSkip : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            GameStateController.Instance.CurrentGameState = GameStateController.GameState.ENTERING_GAME;
            SceneManager.LoadScene(Scenes.LOADING_SCREEN);
        }
    }
}
