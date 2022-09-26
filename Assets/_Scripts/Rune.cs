using System;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    [SerializeField] RuneType runeType;
    [SerializeField] Material activatedMaterial;
    List<RuneListener> listeners;

    void Awake()
    {
        RuneActivator.ActivateRun += ActivateRune;
        listeners = new List<RuneListener>();
    }

    void ActivateRune(RuneType activated)
    {
        if (runeType == activated)
        {
            Debug.Log("Activated!");
            GetComponent<Renderer>().material = activatedMaterial;
            NotifyListeners();
        }
    }

    public void AddListener(RuneListener listener)
    {
        listeners.Add(listener);
    }

    void NotifyListeners()
    {
        foreach(RuneListener listener in listeners)
        {
            listener.OnRuneActivated();
        }
    }

    void OnDestroy()
    {
        RuneActivator.ActivateRun -= ActivateRune;
        listeners.Clear();
    }

    public enum RuneType
    {
        LEFT_WING_RUNE,
        RIGHT_WING_RUNE,
        FRONT_WING_LEFT_RUNE,
        FRONT_WING_FRONT_RUNE,
        FRONT_WING_RIGHT_RUNE
    }
}
