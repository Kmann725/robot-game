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
    [HideInInspector] public int score = 0;
    [HideInInspector] public bool lostHealth = false;
    [HideInInspector] public bool playerRestarted = false;

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

    public void PlayerRestarted()
    {
        if(GameController.Instance.level > 1)
            playerRestarted = true;
    }

    public int GetStarsEarned()
    {
        int stars = 0;
        if (score < 10)
            stars++;
        if (!lostHealth)
            stars++;
        if (!playerRestarted)
            stars++;
        return stars;
    }
}
