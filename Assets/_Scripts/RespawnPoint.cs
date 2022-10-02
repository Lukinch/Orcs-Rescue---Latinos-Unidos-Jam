using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] GameStateController.GameState activetedGameState;

    private void Awake()
    {
        GameStateController.OnGameStateChanged += OnGameStateChanged;
        OnGameStateChanged(GameStateController.Instance.CurrentGameState);
    }

    void OnGameStateChanged(GameStateController.GameState gameState)
    {
            gameObject.SetActive(activetedGameState == gameState);
    }
}
