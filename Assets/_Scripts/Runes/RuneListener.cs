using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rune;

public abstract class RuneListener : MonoBehaviour
{
    [SerializeField] RuneType runeType;

    void Awake()
    {
        RuneActivator.ActivateRun += OnRuneActivated;
    }

    public void OnRuneActivated(RuneType activated)
    {
        if (activated == runeType)
            TriggerMechanism();
    }

    protected abstract void TriggerMechanism();
}
