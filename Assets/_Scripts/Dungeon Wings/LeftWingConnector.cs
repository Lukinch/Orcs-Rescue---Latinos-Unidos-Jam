using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeftWingConnector : MonoBehaviour
{
    [SerializeField] float doorsOffset;
    [SerializeField] float doorsOpeningSpeed;
    [SerializeField] float doorsClosingSpeed;
    [SerializeField] Rigidbody hallConnectionDoor;
    [SerializeField] Rigidbody wingConnectionDoor;

    bool isHallConnectionDoorClosed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            switch(GameStateController.Instance.CurrentGameState)
            {
                case GameStateController.GameState.LEFT_WING_NOT_COMPLETED:
                    if (!isHallConnectionDoorClosed)
                    {
                        isHallConnectionDoorClosed = true;
                        StartCoroutine(CloseGate(hallConnectionDoor));
                        SceneManager.UnloadSceneAsync(Scenes.DUNGEON_MAIN_HALL);
                        StartCoroutine(LoadLeftWing());
                    }
                    break;
                case GameStateController.GameState.LEFT_WING_COMPLETED:
                    if (isHallConnectionDoorClosed)
                    {
                        isHallConnectionDoorClosed = false;
                        StartCoroutine(CloseGate(wingConnectionDoor));
                        SceneManager.UnloadSceneAsync(Scenes.DUNGEON_LEFT_WING);
                        StartCoroutine(LoadHall());
                    }
                    break;
            }
        }
    }

    IEnumerator OpenGate(Rigidbody gate, bool immediately = false)
    {
        Vector3 targetPosition = gate.position + doorsOffset * Vector3.up;

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
        Vector3 targetPosition = gate.position + doorsOffset * Vector3.down;

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

    IEnumerator LoadLeftWing()
    {
        AsyncOperation wingLoadingOperation = SceneManager.LoadSceneAsync(Scenes.DUNGEON_LEFT_WING, LoadSceneMode.Additive);
        while (!wingLoadingOperation.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(OpenGate(wingConnectionDoor));
    }

    IEnumerator LoadHall()
    {
        AsyncOperation hallLoadingOperation = SceneManager.LoadSceneAsync(Scenes.DUNGEON_MAIN_HALL, LoadSceneMode.Additive);
        while (!hallLoadingOperation.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(OpenGate(hallConnectionDoor));
    }
}
