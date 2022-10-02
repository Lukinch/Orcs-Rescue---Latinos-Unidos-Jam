using UnityEngine;
using static Rune;

public class GameStateController : MonoBehaviour
{
    public static GameStateController Instance { get;  private set; }
    public GameState CurrentGameState;
    bool frontWingLeftRuneActivated, frontWingRightRuneActivated, frontWingFrontRuneActivated;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }

        RuneActivator.ActivateRun += OnRuneActivated;
        DontDestroyOnLoad(this);
    }

    void OnRuneActivated(RuneType runeType)
    {
        switch (runeType)
        {
            case RuneType.LEFT_WING_RUNE:
                CurrentGameState = GameState.LEFT_WING_COMPLETED;
                break;
            case RuneType.RIGHT_WING_RUNE:
                CurrentGameState = GameState.RIGHT_WING_COMPLETED;
                break;
            case RuneType.FRONT_WING_LEFT_RUNE:
                frontWingLeftRuneActivated = true;
                CheckFrontWingRunesStates();
                break;
            case RuneType.FRONT_WING_FRONT_RUNE:
                frontWingFrontRuneActivated = true;
                CheckFrontWingRunesStates();
                break;
            case RuneType.FRONT_WING_RIGHT_RUNE:
                frontWingRightRuneActivated = true;
                CheckFrontWingRunesStates();
                break;
            case RuneType.FRONT_WING_RIGHT_INTERMEDIATE_RUNE:
                break;
        }
    }

    void CheckFrontWingRunesStates()
    {
        if (frontWingLeftRuneActivated && frontWingFrontRuneActivated && frontWingRightRuneActivated)
        {
            CurrentGameState = GameState.FRONT_WING_COMPLETED;
        }
    }

    public enum GameState
    {
        OPENING_CINEMATIC,
        ENTERING_GAME,
        LEFT_WING_NOT_COMPLETED,
        LEFT_WING_COMPLETED,
        RIGHT_WING_NOT_COMPLETED,
        RIGHT_WING_COMPLETED,
        FRONT_WING_NOT_COMPLETED,
        FRONT_WING_COMPLETED,
        ENDING_CINEMATIC
    }
}
