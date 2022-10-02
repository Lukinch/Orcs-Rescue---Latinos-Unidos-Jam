using static Rune;

public class FrontWingManager : WingManager
{
    bool firstRuneActivated;
    bool secondRuneActivated;
    bool thirdRuneActivated;

    override protected void OnCinematicStarted()
    {
        base.OnCinematicStarted();
        Invoke(nameof(FadeCamera), 12f);
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
            GameStateController.Instance.CurrentGameState = GameStateController.GameState.FRONT_WING_CHALLENGE_1_COMPLETED;
        } else if (lastActivatedRune == RuneType.FRONT_WING_FRONT_RUNE)
        {
            secondRuneActivated = true;
            GameStateController.Instance.CurrentGameState = GameStateController.GameState.FRONT_WING_CHALLENGE_2_COMPLETED;
        } else if (lastActivatedRune == RuneType.FRONT_WING_RIGHT_RUNE)
        {
            thirdRuneActivated = true;
            GameStateController.Instance.CurrentGameState = GameStateController.GameState.FRONT_WING_CHALLENGE_3_COMPLETED;
        }

        if (firstRuneActivated && secondRuneActivated && thirdRuneActivated)
        {
            GameStateController.Instance.CurrentGameState = GameStateController.GameState.FRONT_WING_COMPLETED;
            return true;
        } else
        {
            return false;
        }
    }
}
