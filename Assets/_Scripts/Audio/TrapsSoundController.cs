using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsSoundController : MonoBehaviour
{
    [SerializeReference] AudioSource _audioSource;

    void OnEnable()
    {
        AudioManager.Instance.OnGamePaused += PauseSound;
        AudioManager.Instance.OnGameResumed += ResumeSound;
    }

    void OnDisable()
    {
        AudioManager.Instance.OnGamePaused -= PauseSound;
        AudioManager.Instance.OnGameResumed -= ResumeSound;
    }

    private void ResumeSound() => _audioSource.UnPause();

    private void PauseSound() => _audioSource.Pause();
}
