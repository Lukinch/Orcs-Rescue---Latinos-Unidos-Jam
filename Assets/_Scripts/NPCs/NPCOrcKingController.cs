using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rune;

public class NPCOrcKingController : MonoBehaviour
{
    Animator animator;
    bool firstRuneActivated;
    bool secondRuneActivated;
    bool thirdRuneActivated;

    void Awake()
    {
        animator = GetComponent<Animator>();
        RuneActivator.ActivateRun += OnRuneActivated;
    }

    public void OnRuneActivated(RuneType activated)
    {
        if (activated == RuneType.FRONT_WING_LEFT_RUNE)
        {
            firstRuneActivated = true;
        } else if (activated == RuneType.FRONT_WING_FRONT_RUNE)
        {
            secondRuneActivated = true;
        } else if (activated == RuneType.FRONT_WING_RIGHT_RUNE)
        {
            thirdRuneActivated = true;
        }

        if (firstRuneActivated && secondRuneActivated && thirdRuneActivated)
        {
            animator.SetTrigger("Celebrate");
        }
            
    }
}
