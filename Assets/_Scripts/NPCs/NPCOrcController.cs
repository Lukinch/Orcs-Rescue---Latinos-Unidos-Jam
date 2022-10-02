using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCOrcController : RuneListener
{
    [SerializeField] int celebrationAnimations = 2;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected override void TriggerMechanism()
    {
        OnOrcRescued();
    }

    void OnOrcRescued()
    {
        animator.SetInteger("Celebration", Random.Range(1, celebrationAnimations+1));
    }
}
