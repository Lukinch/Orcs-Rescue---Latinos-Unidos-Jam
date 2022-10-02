using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnemyController : MonoBehaviour
{
    [SerializeField] int celebrationAnimations = 3;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("Exclamating", Random.Range(0, celebrationAnimations + 1));
    }

}
