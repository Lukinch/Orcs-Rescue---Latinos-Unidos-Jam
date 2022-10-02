using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerSelectorManager;

public class PlayerSelectionButton : MonoBehaviour
{
    [SerializeField] PlayerModel playerModel;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            PlayerSelectorManager.Instance.SelectPlayer(playerModel);
        });
    }
}
