using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameHeadTrap : MonoBehaviour
{
    [SerializeField] float waitingTime;
    [SerializeField] float flameDuration;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip flameClip;
    ParticleSystem flamesParticleSystem;

    void Awake()
    {
        flamesParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        LaunchFlame();
    }

    void LaunchFlame()
    {
        audioSource.Play();
        flamesParticleSystem.Play();
        Invoke(nameof(StopAndRelaunchFlame), flameDuration);
    }

    void StopAndRelaunchFlame()
    {
        audioSource.Stop();
        flamesParticleSystem.Stop();
        Invoke(nameof(LaunchFlame), waitingTime);
    }
}
