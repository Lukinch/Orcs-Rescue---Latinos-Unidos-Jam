using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Oscilator : MonoBehaviour
{
    [SerializeField] float frecuency;
    [SerializeField] float amplitude;
    [SerializeField] Vector3Int movementDirection;
    Vector3 localMovementDirection;
    new Rigidbody rigidbody;
    Vector3 initialPosition;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        SetLocalMovementVector();
    }

    void Update()
    {
        Vector3 newPosition = initialPosition + Mathf.Sin(frecuency * Time.fixedTime) * localMovementDirection;
        rigidbody.MovePosition(newPosition);
    }

    void SetLocalMovementVector()
    {
        localMovementDirection = Vector3.zero;
        if (movementDirection.x != 0)
        {
            localMovementDirection += transform.right;
        }

        if (movementDirection.y != 0)
        {
            localMovementDirection += transform.up;
        }

        if (movementDirection.z != 0)
        {
            localMovementDirection += transform.forward;
        }

        localMovementDirection = amplitude * localMovementDirection;
    }

}
