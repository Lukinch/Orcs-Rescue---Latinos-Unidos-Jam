using UnityEngine;
using UnityEngine.SceneManagement;

public class EndEndingCutscene : MonoBehaviour
{
    private void OnEnable()
    {
        GameStateController.Instance.CurrentGameState = GameStateController.GameState.GAME_COMPLETED;
        SceneManager.LoadScene(Scenes.LOADING_SCREEN);
    }
}
