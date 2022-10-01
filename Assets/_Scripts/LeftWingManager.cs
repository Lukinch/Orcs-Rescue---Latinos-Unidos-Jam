using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rune;

public class LeftWingManager : WingManager
{
    [SerializeField] LeftWingPrisionersPlatform platform;

     override protected void OnCinematicStarted()
    {
        base.OnCinematicStarted();
        platform.TriggerMechanism();
    }

    protected override bool RunesConditionCompleted(RuneType lastActivatedRune)
    {
        return lastActivatedRune == RuneType.LEFT_WING_RUNE;
    }
}
