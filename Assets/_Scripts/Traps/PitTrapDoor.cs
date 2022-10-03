using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PitTrapDoor : MonoBehaviour
{
    [SerializeField] bool isLeftDoor;
    [SerializeField] float openingVelocity;
    new Rigidbody rigidbody;
    Quaternion originalRotation;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        originalRotation = rigidbody.rotation;
    }

    public void Shake(float shakeTime)
    {
        StartCoroutine(ShakeCoroutine(shakeTime));
    }

    public void Open()
    {
        StartCoroutine(OpenCoroutine());
    }

    IEnumerator OpenCoroutine()
    {
        if (isLeftDoor)
        {
            do
            {
                Quaternion delta = Quaternion.Euler(Time.fixedDeltaTime * openingVelocity * -Vector3.forward);
                rigidbody.MoveRotation(rigidbody.rotation * delta);
                yield return new WaitForFixedUpdate();
            }
            while (rigidbody.rotation.eulerAngles.z > 270);
        }
        else
        {
            while (rigidbody.rotation.eulerAngles.z < 90)
            {
                Quaternion delta = Quaternion.Euler(Time.fixedDeltaTime * openingVelocity * Vector3.forward);
                rigidbody.MoveRotation(rigidbody.rotation * delta);
                yield return new WaitForFixedUpdate();
            }
        }
    }

    IEnumerator ShakeCoroutine(float shakeTime)
    {
        int direction = isLeftDoor ? 1 : -1;
        float shakeTimer = 0;
        float directionTimer = 0;
        float toggleTime = 0.2f; // change direction every 0.2s

        while (shakeTimer < shakeTime)
        {
            shakeTimer += Time.deltaTime;
            directionTimer += Time.deltaTime;
            float zAngle = direction * directionTimer * (2 / toggleTime);
            rigidbody.rotation = Quaternion.Euler(0, 0, zAngle);
            if (directionTimer >= toggleTime)
            {
                direction = direction * -1;
                directionTimer = 0;
            }
            yield return null;
        }

        rigidbody.rotation = originalRotation;
    }


    public void ResetTrap()
    {
        StopAllCoroutines();
        rigidbody.rotation = originalRotation;
    }
}
