using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rotator : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 rotationDirection;
    new Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rotationDirection = rotationSpeed * rotationDirection;
    }

    void FixedUpdate()
    {
        Quaternion delta = Quaternion.Euler(Time.fixedDeltaTime * rotationDirection);
        rigidbody.MoveRotation(rigidbody.rotation * delta);
    }
}
