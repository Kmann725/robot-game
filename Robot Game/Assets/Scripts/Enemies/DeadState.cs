using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IEnemyState
{
    private Enemy enemy;

    public DeadState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void EnterState()
    {
        // Play Death Animation
        enemy.enemyAnimator.Play("Die");
    }

    public void UpdateState()
    {

    }

    public void ExitState()
    {

    }
}
