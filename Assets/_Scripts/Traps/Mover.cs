using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] float moveToTargetVelocity;
    [SerializeField] float backToInitVelocity;
    [SerializeField] float waitTime;
    [SerializeField] Vector3 movementDelta;

    new Rigidbody rigidbody;
    Vector3 initialPosition;
    Vector3 targetPosition;
    bool isFromInitialPosition;
    bool shouldWait;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        initialPosition = rigidbody.position;
        SetLocalMovementVector();
    }

    void FixedUpdate()
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

    void ChangeMovement()
    {
        isFromInitialPosition = !isFromInitialPosition;
        shouldWait = true;
        Invoke("StartMovement", waitTime);
    }

    void StartMovement()
    {
        shouldWait = false;
    }

    void SetLocalMovementVector()
    {
        targetPosition = rigidbody.position + movementDelta;
    }
}
