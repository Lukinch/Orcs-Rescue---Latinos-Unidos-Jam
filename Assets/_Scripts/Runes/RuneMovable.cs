using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneMovable : RuneListener
{
    [SerializeField] Vector3 direction;
    [SerializeField] float distance;
    [SerializeField] float movingSpeed;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip soundToPlay;

    protected override void TriggerMechanism()
    {
        if (movingSpeed == 0)
        {
            // Instant movement
            transform.position += distance * direction;
        }
        else
        {
            if (soundToPlay)
            {
                audioSource.clip = soundToPlay;
                audioSource.loop = false;
                audioSource.Play();
            }
            StartCoroutine(MoveObstacleCoroutine());
        }
    }

    IEnumerator MoveObstacleCoroutine()
    {
        Vector3 target = transform.position + distance * direction;

        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, movingSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        if (soundToPlay) audioSource.Stop();
        Debug.Log("Obstacle Moved");
    }
}
