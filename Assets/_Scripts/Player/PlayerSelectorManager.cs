using System;
using UnityEngine;

public class PlayerSelectorManager : MonoBehaviour
{
    public static PlayerSelectorManager Instance;

    PlayerModel currentPlayerModel;
    public PlayerModel CurrentPlayerModel
    {
        get => currentPlayerModel;
        set
        {
            currentPlayerModel = value;
            OnPlayerSelectionChanged?.Invoke(currentPlayerModel);
        }
    }

    public static event Action<PlayerModel> OnPlayerSelectionChanged;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            CurrentPlayerModel = (PlayerModel) PlayerPrefs.GetInt("PlayerModel", 0);
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SelectPlayer(PlayerModel playerModel)
    {
        PlayerPrefs.SetInt("PlayerModel", (int)playerModel);
        CurrentPlayerModel = playerModel;
    }

    public enum PlayerModel
    {
        MALE_HERO,
        FEMALE_HERO,
        MALE_ORC_HERO,
        FEMALE_ORC_HERO
    }
}
