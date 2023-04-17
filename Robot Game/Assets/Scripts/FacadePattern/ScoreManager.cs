/*
 * Gerard Lamoureux
 * ScoreManager
 * Team Project 2
 * Keeps track of enemies killed
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [HideInInspector] public int score;

    public void IncrementScore(int amount)
    {
        score += amount;
    }
}
