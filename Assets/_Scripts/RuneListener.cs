using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rune;

public abstract class RuneListener : MonoBehaviour
{
    [SerializeField] RuneType runeType;

    void Awake()
    {
        Rune.OnRuneActivated += OnRuneActivated;
    }

    void OnRuneActivated(RuneType activated)
    {
        if (runeType != activated)
        {
            TriggerMechanism();
        }
    }

    protected abstract void TriggerMechanism();
}
