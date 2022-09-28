using System;
using System.Collections;
using UnityEngine;
using static Rune;

public class RuneActivator : MonoBehaviour
{
    [SerializeField] RuneType runeType;
    [SerializeField] float animationOffset;
    [SerializeField] float animationSpeed;
    [SerializeField] Material activatedMaterial;
    bool alreadyActivated;
    public static event Action<RuneType> ActivateRun;

    void OnTriggerEnter(Collider other)
    {
        if (!alreadyActivated && other.CompareTag(Tags.PLAYER))
        {
            alreadyActivated = true;
            StartCoroutine(ActivateRuneCoroutine());
        }
    }

    IEnumerator ActivateRuneCoroutine()
    {
        Vector3 activatedPosition = new Vector3(transform.position.x, transform.position.y + animationOffset, transform.position.z);
        while (transform.position != activatedPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, activatedPosition, animationSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        GetComponent<Renderer>().material = activatedMaterial;
        ActivateRun?.Invoke(runeType);
    }
}
