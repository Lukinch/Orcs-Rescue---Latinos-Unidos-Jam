using System;
using System.Collections;
using UnityEngine;
using NaughtyAttributes;
using static Rune;

public class RuneActivator : MonoBehaviour
{
    [SerializeField] RuneType runeType;
    [SerializeField] float animationOffset;
    [SerializeField] float animationSpeed;
    float maxWaitTime = 0.8f;
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
        float counter = 0;
        Vector3 activatedPosition = new Vector3(transform.position.x, transform.position.y + animationOffset, transform.position.z);
        Debug.Log("Activating!");
        while (transform.position != activatedPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, activatedPosition, animationSpeed * Time.deltaTime);
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            if (counter > maxWaitTime) break;
        }
        Debug.Log("Activated!");
        GetComponent<Renderer>().material = activatedMaterial;
        ActivateRun?.Invoke(runeType);
    }

#if UNITY_EDITOR
    [Button]
    void TestRuneActivation()
    {
        StartCoroutine(ActivateRuneCoroutine());
    }
#endif
}
