using System;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [Inject]
    public IPlayerStatsModifier playerStatsModifier;
    
    private void Start()
    {
        //playerStatsModifier.SetPlayer(playerStatsSaveLoader.GetPlayer());
    }
}