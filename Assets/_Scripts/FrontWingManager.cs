using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rune;

public class FrontWingManager : WingManager
{
    bool firstRuneActivated;
    bool secondRuneActivated;
    bool thirdRuneActivated;

    override protected void OnCinematicStarted()
    {
        base.OnCinematicStarted();
        Invoke(nameof(FadeCamera), 15f);
    }

    void FadeCamera()
    {
        cinematicCamera.Fade();
    }

    protected override bool RunesConditionCompleted(RuneType lastActivatedRune)
    {
        if (lastActivatedRune == RuneType.FRONT_WING_LEFT_RUNE)
        {
            firstRuneActivated = true;
        } else if (lastActivatedRune == RuneType.FRONT_WING_FRONT_RUNE)
        {
            secondRuneActivated = true;
        } else if (lastActivatedRune == RuneType.FRONT_WING_RIGHT_RUNE)
        {
            thirdRuneActivated = true;
        }

        return firstRuneActivated && secondRuneActivated && thirdRuneActivated;
    }
}
