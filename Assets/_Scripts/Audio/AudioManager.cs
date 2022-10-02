using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _backgroundAudioClips;

    public static AudioManager Instance { get; private set; }
    public AudioSource AudioSource { get => _audioSource; }

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
        isPlayingLevelBGM = true;
    }

    void Update()
    {
        if (!isPlayingLevelBGM) return;

        if ((_audioSource.clip.length - _audioSource.time) < 1)
        {
            int index = Random.Range(0, _backgroundAudioClips.Length);
            while (_audioSource.clip == _backgroundAudioClips[index])
            {
                index = Random.Range(0, _backgroundAudioClips.Length);
            }
            ChangeCurrentTrack(_backgroundAudioClips[index]);
        }
    }

    public void Pause() => _audioSource.Pause();
    public void Resume()
    {
        _audioSource.UnPause();
        isPlayingLevelBGM = true;
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
        int index = Random.Range(0, _backgroundAudioClips.Length);
        ChangeCurrentTrack(_backgroundAudioClips[index]);
        isPlayingLevelBGM = true;
    }
}
