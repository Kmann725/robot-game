using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IEnemyState
{
    private Enemy enemy;
    private bool playerInSight = true;

    public ChaseState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void EnterState()
    {
        enemy.navMeshAgent.SetDestination(enemy.target.transform.position);
    }

    public void UpdateState()
    {
        playerInSight = enemy.IsPlayerInSight();

        if(enemy.target == null || !playerInSight)
            enemy.SetState(enemy.wanderState);
        else if(playerInSight && Vector3.Distance(enemy.transform.position, enemy.target.transform.position) < enemy.attackRadius)
            enemy.SetState(enemy.attackState);

        if (playerInSight)
            enemy.navMeshAgent.SetDestination(enemy.target.transform.position);
    }

    public void ExitState()
    {
        enemy.navMeshAgent.ResetPath();
    }
}
