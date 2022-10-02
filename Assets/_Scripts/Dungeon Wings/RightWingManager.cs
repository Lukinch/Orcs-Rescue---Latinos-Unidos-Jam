using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rune;

public class RightWingManager : WingManager
{
    override protected void OnCinematicStarted()
    {
        base.OnCinematicStarted();
        Invoke(nameof(FadeCamera), 5.5f);
    }

    void FadeCamera()
    {
        cinematicCamera.Fade();
    }

    protected override bool RunesConditionCompleted(RuneType lastActivatedRune)
    {
        return lastActivatedRune == RuneType.RIGHT_WING_RUNE;
    }
}
