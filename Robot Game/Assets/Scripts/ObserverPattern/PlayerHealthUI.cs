/*
 * Gerard Lamoureux
 * PlayerHealthUI
 * Team Project 1
 * Handles the Player Health Bar
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour, IPlayerObserver
{
    private Slider healthBar;
    void Awake()
    {
        healthBar = GetComponent<Slider>();
    }

    void Start()
    {
        PlayerController.Instance.RegisterPlayerObserver(this);
    }

    public void UpdateData(PlayerData pd)
    {
        healthBar.value = (float)pd.PlayerHealth / (float)PlayerController.Instance.maxHealth;
    }
}
