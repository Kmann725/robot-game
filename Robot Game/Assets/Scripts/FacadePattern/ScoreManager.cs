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

    public void IncrementScore(int amount)
    {
        score += amount;
    }
}
