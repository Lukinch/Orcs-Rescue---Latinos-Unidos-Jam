using UnityEngine;

public class NPCCelebrationController : MonoBehaviour
{
    [SerializeField] int celebrationAnimations = 2;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("Celebration", Random.Range(1, celebrationAnimations + 1));
    }
}
