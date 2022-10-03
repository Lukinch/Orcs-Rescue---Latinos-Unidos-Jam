using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject text;

    bool _shouldListenToInput;

    void Start()
    {
        _shouldListenToInput = false;
        text.SetActive(false);
    }

    void Update()
    {
        if (!_shouldListenToInput) return;

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

    public void EnableInputs()
    {
        _shouldListenToInput = true;
        text.SetActive(true);
    }
}
