using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _footstepClips;

    public void PlayFootStepSound()
    {
        int index = Random.Range(0, _footstepClips.Length);
        _audioSource.PlayOneShot(_footstepClips[index], _audioSource.volume);
    }
}
