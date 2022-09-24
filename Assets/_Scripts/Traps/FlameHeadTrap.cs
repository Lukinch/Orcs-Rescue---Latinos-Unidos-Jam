using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameHeadTrap : MonoBehaviour
{
    [SerializeField] float waitingTime;
    [SerializeField] float flameDuration;
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
        flamesParticleSystem.Play();
        Invoke(nameof(StopAndRelaunchFlame), flameDuration);
    }

    void StopAndRelaunchFlame()
    {
        flamesParticleSystem.Stop();
        Invoke(nameof(LaunchFlame), waitingTime);
    }
}
