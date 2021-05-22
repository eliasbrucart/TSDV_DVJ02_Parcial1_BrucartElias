﻿using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour
{
    [SerializeField] private TimerManager timeManager;
    [SerializeField] private EnemySpawner enemySpawner;
    public Text timeLeftText;
    public Text pointsText;
    public Text enemiesLeftText;
    void Start()
    {
        
    }

    void Update()
    {
        timeLeftText.text = "Time played: " + (int)timeManager.timer;
        pointsText.text = "Points earned: " + GameManager.instanceGameManager.points;
        enemiesLeftText.text = "Enemy Left: " + enemySpawner.enemiesAlive;
    }
}
