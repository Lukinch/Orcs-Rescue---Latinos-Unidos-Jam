using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _mainMenuClip;
    [SerializeField] AudioClip[] _backgroundAudioClips;

    public static AudioManager Instance { get; private set; }
    public AudioSource AudioSource { get => _audioSource; }

    public event Action OnGamePaused;
    public event Action OnGameResumed;

    bool isPlayingLevelBGM;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (!isPlayingLevelBGM) return;

        if ((_audioSource.clip.length - _audioSource.time) < Mathf.Epsilon)
        {
            int index = UnityEngine.Random.Range(0, _backgroundAudioClips.Length);
            while (_audioSource.clip == _backgroundAudioClips[index])
            {
                index = UnityEngine.Random.Range(0, _backgroundAudioClips.Length);
            }
            ChangeCurrentTrack(_backgroundAudioClips[index]);
        }
    }

    public void Pause()
    {
        _audioSource.Pause();
        OnGamePaused?.Invoke();
    }
    public void Resume()
    {
        _audioSource.UnPause();
        isPlayingLevelBGM = true;
        OnGameResumed?.Invoke();
    }
    public void Stop()
    {
        _audioSource.Stop();
        isPlayingLevelBGM = false;
    }
    public void ChangeCurrentTrack(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        _audioSource.loop = true;
        _audioSource.Play();
    }
    public void StartPlayingBGM()
    {
        int index = UnityEngine.Random.Range(0, _backgroundAudioClips.Length);
        ChangeCurrentTrack(_backgroundAudioClips[index]);
        _audioSource.loop = false;
        isPlayingLevelBGM = true;
    }
    void PlayMainMenuMusic()
    {
        _audioSource.clip = _mainMenuClip;
        _audioSource.loop = true;
        _audioSource.Play();
    }
    public void PlayMainMenuMusicDelayed()
    {
        Invoke(nameof(PlayMainMenuMusic), 0.2f);
    }
}
