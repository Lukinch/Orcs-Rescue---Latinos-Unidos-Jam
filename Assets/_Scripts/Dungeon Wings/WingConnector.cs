using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WingConnector : MonoBehaviour
{
    [SerializeField] float doorsOffset;
    [SerializeField] float doorsOpeningSpeed;
    [SerializeField] float doorsClosingSpeed;
    [SerializeField] Rigidbody hallConnectionDoor;
    [SerializeField] Rigidbody wingConnectionDoor;
    [SerializeField] GameStateController.GameState wingIncompletedState;
    [SerializeField] GameStateController.GameState wingCompletedState;
    [SerializeField] string wingScene;

    bool isHallConnectionDoorClosed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            if (GameStateController.Instance.CurrentGameState == wingIncompletedState)
            {
                if (!isHallConnectionDoorClosed)
                {
                    isHallConnectionDoorClosed = true;
                    StartCoroutine(CloseGate(hallConnectionDoor));
                    SceneManager.UnloadSceneAsync(Scenes.DUNGEON_MAIN_HALL);
                    StartCoroutine(LoadWing());
                }
            }
            else if (GameStateController.Instance.CurrentGameState == wingCompletedState)
            {
                if (isHallConnectionDoorClosed)
                {
                    isHallConnectionDoorClosed = false;
                    StartCoroutine(CloseGate(wingConnectionDoor));
                    SceneManager.UnloadSceneAsync(wingScene);
                    StartCoroutine(LoadHall());
                }
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

    IEnumerator LoadWing()
    {
        AsyncOperation wingLoadingOperation = SceneManager.LoadSceneAsync(wingScene, LoadSceneMode.Additive);
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
