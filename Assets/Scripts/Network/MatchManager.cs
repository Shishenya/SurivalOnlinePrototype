using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MatchManager : MonoBehaviour
{
    public static MatchManager Instance;
    public Dictionary<string, PlayerComponent> AllPlayerComponent = new Dictionary<string, PlayerComponent>();   

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Инициализация ссылок на даные игрока
    /// </summary>
    public void InitPlayer(string nickname, PlayerController playerController, PlayerInventory playerInventory)
    {
        PlayerComponent playerComponent = new PlayerComponent { nickname = nickname, playerController = playerController, playerInventory = playerInventory };
        if (!AllPlayerComponent.ContainsKey(nickname))
        {
            AllPlayerComponent.Add(nickname, playerComponent);
        }
    }

    /// <summary>
    /// Получение информации по ID
    /// </summary>
    public PlayerComponent GetPlayerComponent(string nickname)
    {
        if (AllPlayerComponent.ContainsKey(nickname))
        {
            return AllPlayerComponent[nickname];
        }
        else
        {
            return null;
        }


    }

}
