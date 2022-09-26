using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuneListener : MonoBehaviour
{
    [SerializeField] Rune rune;

    void Start()
    {
        rune.AddListener(this);
    }

    public void OnRuneActivated()
    {
        TriggerMechanism();
    }

    protected abstract void TriggerMechanism();
}
