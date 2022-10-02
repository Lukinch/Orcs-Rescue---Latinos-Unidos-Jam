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
        if (PlayerManager.Instance) PlayerManager.Instance.ClearPlayer();

        SceneManager.LoadScene(Scenes.MAIN_MENU);
    }

    public void EnableInputs()
    {
        _shouldListenToInput = true;
        text.SetActive(true);
    }
}
