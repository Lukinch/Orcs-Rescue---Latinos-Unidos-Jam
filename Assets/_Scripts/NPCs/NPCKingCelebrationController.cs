using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCKingCelebrationController : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Celebrate");
    }
}
