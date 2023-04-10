using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockState : IEnemyState
{
    private Enemy enemy;

    public ShockState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void EnterState()
    {
        // Play Death Animation
        enemy.enemyAnimator.Play("Idle_Ducking_AR");
        enemy.StartShockCoroutine();
    }

    public void UpdateState()
    {

    }

    public void ExitState()
    {
        
    }
}
