using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonHallManager : MonoBehaviour
{
    [SerializeField] Transform playerInitialSpawnPoint;
    [SerializeField] float doorsOpeningSpeed;
    [SerializeField] float doorsClosingSpeed;
    [SerializeField] Rigidbody leftWingGate;
    [SerializeField] Rigidbody rightWingGate;
    [SerializeField] Rigidbody frontWingGate;

    void Awake()
    {
        CheckState();
    }

    void CheckState()
    {
        switch (GameStateController.Instance.CurrentGameState)
        {
            case GameStateController.GameState.ENTERING_GAME:
                OnGameInit();
                break;
            case GameStateController.GameState.LEFT_WING_NOT_COMPLETED:
                StartCoroutine(OpenGate(leftWingGate));
                break;
            case GameStateController.GameState.LEFT_WING_COMPLETED:
                StartCoroutine(OpenGate(leftWingGate, true));
                break;
            case GameStateController.GameState.RIGHT_WING_NOT_COMPLETED:
                StartCoroutine(LoadRightWingConnector());
                break;
            case GameStateController.GameState.RIGHT_WING_COMPLETED:
                StartCoroutine(OpenGate(rightWingGate, true));
                break;
            case GameStateController.GameState.FRONT_WING_NOT_COMPLETED:
                StartCoroutine(LoadFrontWingConnector());
                break;
            case GameStateController.GameState.FRONT_WING_COMPLETED:
                StartCoroutine(OpenGate(frontWingGate, true));
                break;
        }
    }

    void OnGameInit()
    {
        AudioManager.Instance.StartPlayingBGM();
        PlayerManager.Instance.PlayerInput.gameObject.transform.SetPositionAndRotation(
                    playerInitialSpawnPoint.position, playerInitialSpawnPoint.rotation);
        PlayerManager.Instance.PlayerReferences.EnableVisuals();
        PlayerManager.Instance.PlayerReferences.EnableCameras();
        PlayerManager.Instance.PlayerReferences.EnableControllerScript();
        PlayerManager.Instance.PlayerReferences.MakePlayerDynamic();
        PlayerManager.Instance.PlayerInput.SwitchCurrentActionMap("Player");

        EnableLeftWing();
    }
    public void OnLeftWingLeft()
    {
        StartCoroutine(CloseGate(leftWingGate));
        SceneManager.UnloadSceneAsync(Scenes.DUNGEON_LEFT_WING_CONNECTOR);
        EnableRightWing();
    }

    public void OnRightWingLeft()
    {
        StartCoroutine(CloseGate(rightWingGate));
        SceneManager.UnloadSceneAsync(Scenes.DUNGEON_RIGHT_WING_CONNECTOR);
        EnableFrontWing();
    }

    public void OnFrontWingLeft()
    {
        StartCoroutine(CloseGate(frontWingGate));
        SceneManager.UnloadSceneAsync(Scenes.DUNGEON_FRONT_WING_CONNECTOR);
    }

    void EnableLeftWing()
    {
        GameStateController.Instance.CurrentGameState = GameStateController.GameState.LEFT_WING_NOT_COMPLETED;
        CheckState();
    }

    void EnableRightWing()
    {
        GameStateController.Instance.CurrentGameState = GameStateController.GameState.RIGHT_WING_NOT_COMPLETED;
        CheckState();
    }

    void EnableFrontWing()
    {
        GameStateController.Instance.CurrentGameState = GameStateController.GameState.FRONT_WING_NOT_COMPLETED;
        CheckState();
    }

    IEnumerator LoadRightWingConnector()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(Scenes.DUNGEON_RIGHT_WING_CONNECTOR, LoadSceneMode.Additive);
        while (!asyncOperation.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(OpenGate(rightWingGate));
    }

    IEnumerator LoadFrontWingConnector()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(Scenes.DUNGEON_FRONT_WING_CONNECTOR, LoadSceneMode.Additive);
        while (!asyncOperation.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(OpenGate(frontWingGate));
    }

    IEnumerator OpenGate(Rigidbody gate, bool immediately = false)
    {
        Vector3 targetPosition = gate.position + 5 * Vector3.down;

        if (immediately)
        {
            gate.MovePosition(targetPosition);
        }
        else
        {
            while (gate.transform.position != targetPosition)
            {
                Vector3 newPos = Vector3.MoveTowards(gate.transform.position, targetPosition, doorsOpeningSpeed * Time.fixedDeltaTime);
                gate.MovePosition(newPos);
                yield return new WaitForFixedUpdate();
            }
        }
    }

    IEnumerator CloseGate(Rigidbody gate, bool immediately = false)
    {
        Vector3 targetPosition = gate.position + 5 * Vector3.up;

        if (immediately)
        {
            gate.MovePosition(targetPosition);
        }
        else
        {
            while (gate.transform.position != targetPosition)
            {
                Vector3 newPos = Vector3.MoveTowards(gate.transform.position, targetPosition, doorsClosingSpeed * Time.fixedDeltaTime);
                gate.MovePosition(newPos);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
