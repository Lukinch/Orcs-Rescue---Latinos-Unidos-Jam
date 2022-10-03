using System;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using NaughtyAttributes;
#endif
using static Rune;

public class GameStateController : MonoBehaviour
{
    public static GameStateController Instance { get;  private set; }
    GameState currentGameState;
    public GameState CurrentGameState
    {
        get => currentGameState; 
        set
        {
            currentGameState = value;
            OnGameStateChanged?.Invoke(currentGameState);
        }
    }

    public static event Action<GameState> OnGameStateChanged;

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

    public void OnBackToMainMenu()
    {
        CurrentGameState = GameState.MAIN_MENU;
    }

    public void OnAllOrcsRescued()
    {
        PlayerPrefs.SetInt("GameWon", 1);
        CurrentGameState = GameState.ENDING_CINEMATIC;
        SceneManager.LoadScene(Scenes.LOADING_SCREEN);
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
        ENDING_CINEMATIC,
        GAME_COMPLETED,
        MAIN_MENU,
        FRONT_WING_CHALLENGE_1_COMPLETED,
        FRONT_WING_CHALLENGE_2_COMPLETED,
        FRONT_WING_CHALLENGE_3_COMPLETED
    }

#if UNITY_EDITOR
    [Button]
    void SimulateGameWon()
    {
        OnAllOrcsRescued();
    }
#endif
}
