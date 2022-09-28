using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rune;

public class RuneElevator : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float distance;
    [SerializeField] float movingSpeed;
    [SerializeField] RuneType firstRuneType;
    [SerializeField] RuneType secondRuneType;
    [SerializeField] RuneType thirdRuneType;
    bool firstRunActivated;
    bool secondRunActivated;
    bool thirdRunActivated;

    void Awake()
    {
        RuneActivator.ActivateRun += OnRuneActivated;
    }

    public void OnRuneActivated(RuneType activated)
    {
        if (activated == firstRuneType)
        {
            firstRunActivated = true;
        }
        else if (activated == secondRuneType)
        {
            secondRunActivated = true;
        }
        else if (activated == thirdRuneType)
        {
            thirdRunActivated = true;
        }

        if (firstRunActivated && secondRunActivated && thirdRunActivated)
        {
            StartCoroutine(MoveElevatorCoroutine());
        }
    }

    IEnumerator MoveElevatorCoroutine()
    {
        Vector3 target = transform.position + distance * direction;

        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, movingSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Elevator Moved");
    }

}
