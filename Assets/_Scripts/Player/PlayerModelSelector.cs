using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerSelectorManager;

public class PlayerModelSelector : MonoBehaviour
{
    [SerializeField] GameObject maleHero;
    [SerializeField] GameObject femaleHero;
    [SerializeField] GameObject maleOrcHero;
    [SerializeField] GameObject femaleOrcHero;

    void Awake()
    {
        PlayerSelectorManager.OnPlayerSelectionChanged += EnableSelectedModel;
        EnableSelectedModel(PlayerSelectorManager.Instance.CurrentPlayerModel);
    }

    public void EnableSelectedModel(PlayerModel playerModel)
    {
        maleHero.SetActive(playerModel == PlayerModel.MALE_HERO);
        femaleHero.SetActive(playerModel == PlayerModel.FEMALE_HERO);
        maleOrcHero.SetActive(playerModel == PlayerModel.MALE_ORC_HERO);
        femaleOrcHero.SetActive(playerModel == PlayerModel.FEMALE_ORC_HERO);
    }

    void OnDestroy()
    {
        PlayerSelectorManager.OnPlayerSelectionChanged -= EnableSelectedModel;
    }
}
