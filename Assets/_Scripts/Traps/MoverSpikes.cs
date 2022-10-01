using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverSpikes : Mover
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] pikeClips;

    protected override void FixedUpdate()
    {
        if (isTrapActive)
        {
            if (!shouldWait)
            {
                if (isFromInitialPosition)
                {
                    rigidbody.MovePosition(Vector3.MoveTowards(rigidbody.position, targetPosition, moveToTargetVelocity * Time.fixedDeltaTime));
                    if (rigidbody.position == targetPosition)
                    {
                        audioSource.PlayOneShot(pikeClips[Random.Range(0, pikeClips.Length)]);
                        ChangeMovement();
                    }
                }
                else
                {
                    rigidbody.MovePosition(Vector3.MoveTowards(rigidbody.position, initialPosition, backToInitVelocity * Time.fixedDeltaTime));
                    if (rigidbody.position == initialPosition)
                    {
                        ChangeMovement();
                    }
                }
            }
        }
    }

    protected override void StartMovement()
    {
        base.StartMovement();
    }
}
