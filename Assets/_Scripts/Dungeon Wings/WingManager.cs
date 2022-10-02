using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rune;

public abstract class WingManager : MonoBehaviour
{
    [SerializeField] GameObject OrcsGroup;
    [SerializeField] protected FadeCamera cinematicCamera;
    PlayerManager playerManager;

    void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        RuneActivator.ActivateRun += OnRuneActivated;
    }

    void OnRuneActivated(RuneType activated)
    {
        if (RunesConditionCompleted(activated))
        {
            Invoke(nameof(OnCinematicStarted), 0.5f);
        }
    }

    protected abstract bool RunesConditionCompleted(RuneType lastActivatedRune);

    protected virtual void OnCinematicStarted()
    {
        playerManager.DisablePlayerInteractionAndCamera();
        cinematicCamera.gameObject.SetActive(true);
    }

    public void OnCinematicEnded()
    {
        cinematicCamera.gameObject.SetActive(false);
        Destroy(OrcsGroup);
        playerManager.EnablePlayerInteractionAndCamera();
    }
}
