using System.Collections;
using UnityEngine;

public class PitTrap : MonoBehaviour
{
    [SerializeField] float timeToOpen;
    [SerializeField] float shakeTime;
    PitTrapDoor[] trapDoors;
    bool trapOpened;

    void Awake()
    {
        trapDoors = GetComponentsInChildren<PitTrapDoor>();
    }

    void OpenTrapDoors()
    {
        trapOpened = true;
        foreach (PitTrapDoor trapDoor in trapDoors)
        {
            trapDoor.Open();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!trapOpened && other.CompareTag(Tags.PLAYER))
        {
            foreach (PitTrapDoor trapDoor in trapDoors)
            {
                trapDoor.Shake(shakeTime);
            }

            StartCoroutine(WaitForTimerCoroutine());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!trapOpened && other.CompareTag(Tags.PLAYER))
        {
            StopCoroutine(WaitForTimerCoroutine());
        }
    }

    IEnumerator WaitForTimerCoroutine()
    {
        yield return new WaitForSecondsRealtime(timeToOpen);
        OpenTrapDoors();
    }
}
