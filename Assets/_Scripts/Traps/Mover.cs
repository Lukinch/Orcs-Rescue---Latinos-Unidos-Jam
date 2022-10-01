using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] protected float moveToTargetVelocity;
    [SerializeField] protected float backToInitVelocity;
    [SerializeField] protected float waitTime;
    [SerializeField] protected float startDelay;
    [SerializeField] protected Vector3 movementDelta;

    protected new Rigidbody rigidbody;
    protected Vector3 initialPosition;
    protected Vector3 targetPosition;
    protected bool isFromInitialPosition;
    protected bool shouldWait;
    protected bool isTrapActive;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        initialPosition = rigidbody.position;
        SetLocalMovementVector();

        if (startDelay > 0)
        {
            Invoke(nameof(ActivateTrap), startDelay);
        }
        else
        {
            ActivateTrap();
        }
    }

    void ActivateTrap()
    {
        isTrapActive = true;
    }

    protected virtual void FixedUpdate()
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

    protected void ChangeMovement()
    {
        isFromInitialPosition = !isFromInitialPosition;
        shouldWait = true;
        Invoke("StartMovement", waitTime);
    }

    protected virtual void StartMovement()
    {
        shouldWait = false;
    }

    protected void SetLocalMovementVector()
    {
        targetPosition = rigidbody.position + movementDelta;
    }
}
