using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rune;

public class RuneClosedDoor : RuneListener
{
    [SerializeField] float openRotationOffset;
    [SerializeField] float openRotationSpeed;

    protected override void TriggerMechanism()
    {
        if (openRotationSpeed == 0)
        {
            // Instant rotation
            transform.rotation *= Quaternion.Euler(0, openRotationOffset, 0);
        } else
        {
            StartCoroutine(OpenDoorCoroutine());
        }
    }

    IEnumerator OpenDoorCoroutine()
    {
        Quaternion target = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + openRotationOffset, transform.eulerAngles.z);
        
        while (!transform.rotation.Approximately(target))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target, openRotationSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Door Opened");
    }
}
