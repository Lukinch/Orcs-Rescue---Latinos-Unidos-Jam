using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pendule : MonoBehaviour
{
    [SerializeField] float frecuency;
    [SerializeField] float amplitude;
    [SerializeField] Vector3 rotationDirection;
    Quaternion initialRotation;
    new Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        initialRotation = rigidbody.rotation;
        rotationDirection = amplitude * rotationDirection;
    }

    void FixedUpdate()
    {
        Quaternion delta = Quaternion.Euler(Mathf.Sin(frecuency * Time.fixedTime) * rotationDirection);
        rigidbody.MoveRotation(initialRotation * delta);
    }
}
