using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWingTrigger : MonoBehaviour
{
    [SerializeField] DungeonHallManager hallManager;

    void Awake()
    {
        if (GameStateController.Instance.CurrentGameState != GameStateController.GameState.ENTERING_GAME &&
            GameStateController.Instance.CurrentGameState != GameStateController.GameState.LEFT_WING_NOT_COMPLETED &&
            GameStateController.Instance.CurrentGameState != GameStateController.GameState.LEFT_WING_COMPLETED)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER) && GameStateController.Instance.CurrentGameState == GameStateController.GameState.LEFT_WING_COMPLETED)
        {
            hallManager.OnLeftWingLeft();
            Destroy(gameObject);
        }
    }
}
