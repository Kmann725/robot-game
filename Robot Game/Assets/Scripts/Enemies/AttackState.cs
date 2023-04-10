using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{
    private Enemy enemy;

    public AttackState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void EnterState()
    {
        enemy.enemyAnimator.Play("Idle_Shoot_Ar");
        enemy.StartAttackCoroutine();
    }

    public void UpdateState()
    {
        enemy.navMeshAgent.ResetPath();
        Vector3 directionToPlayer = enemy.target.transform.position - enemy.transform.position;
        directionToPlayer.y = 0;
        enemy.transform.rotation = Quaternion.LookRotation(directionToPlayer);
        if (enemy.target == null || !enemy.IsPlayerInSight())
            enemy.SetState(enemy.wanderState);
        else if (Vector3.Distance(enemy.transform.position, enemy.target.transform.position) > enemy.attackRadius + 5)
            enemy.SetState(enemy.chaseState);
    }

    public void ExitState()
    {
        enemy.StopAttackCoroutine();
    }
}
