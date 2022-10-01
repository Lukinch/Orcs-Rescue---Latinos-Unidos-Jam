using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWingPrisionersPlatform : MonoBehaviour
{
    [SerializeField] float downDistance;
    [SerializeField] float forwardDistance;
    [SerializeField] float movingSpeed;
    [SerializeField] float openDoorSpeed;
    [SerializeField] Transform platform;
    [SerializeField] Transform gate;

    public void TriggerMechanism()
    {
        StartCoroutine(MovePlatformDownCoroutine());
    }

    IEnumerator MovePlatformDownCoroutine()
    {
        Vector3 target = platform.position + downDistance * Vector3.down;

        while (platform.position != target)
        {
            platform.position = Vector3.MoveTowards(platform.position, target, movingSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(MovePlatformForwardCoroutine());
    }

    IEnumerator MovePlatformForwardCoroutine()
    {
        Vector3 target = transform.position + forwardDistance * Vector3.left;

        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, movingSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(OpenGateCoroutine());
    }

    IEnumerator OpenGateCoroutine()
    {
        while (gate.eulerAngles.x < 89)
        {
            Quaternion delta = Quaternion.Euler(Time.fixedDeltaTime * openDoorSpeed * Vector3.right);
            gate.rotation *=  delta;
            yield return new WaitForFixedUpdate();
        }

        FindObjectOfType<FadeCamera>().Fade();
    }
}
