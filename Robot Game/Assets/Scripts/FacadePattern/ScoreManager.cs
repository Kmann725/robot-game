/*
 * Gerard Lamoureux
 * ScoreManager
 * Team Project 2
 * Keeps track of enemies killed
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [HideInInspector] public int score;
    [HideInInspector] public bool lostHealth = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void IncrementScore(int amount)
    {
        score += amount;
    }

    public void PlayerLostHealth()
    {
        lostHealth = true;
    }
}
