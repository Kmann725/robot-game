/*
 * Gerard Lamoureux
 * PlayerData
 * Team Project 2
 * Handles PlayerData to be sent for Observers
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int PlayerHealth;
    public int NormalGrenadeCount;
    public int EMPGrenadeCount;
    public int GravityWellGrenadeCount;
    public bool IsPlayerSliding;
    public bool IsPlayerTeleporting;
    public bool PlayerSpeedBuffed;
    public bool PlayerSpeedDebuffed;

    public PlayerData(int health = 100)
    {
        PlayerHealth = health;
        NormalGrenadeCount = 0;
        EMPGrenadeCount = 0;
        GravityWellGrenadeCount = 0;
    }
}