using System;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    [SerializeField] RuneType runeType;
    [SerializeField] Material activatedMaterial;

    void Awake()
    {
        RuneActivator.ActivateRun += ActivateRune;
    }

    void ActivateRune(RuneType activated)
    {
        if (runeType == activated)
        {
            Debug.Log("Activated!");
            GetComponent<Renderer>().material = activatedMaterial;
        }
    }

    void OnDestroy()
    {
        RuneActivator.ActivateRun -= ActivateRune;
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
