using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderState : IEnemyState
{
    private Enemy enemy;

    public WanderState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    /// <summary>
    /// Immediately sets the enemies' destination to a random location around them when the state is entered.
    /// </summary>
    public void EnterState()
    {
        enemy.navMeshAgent.SetDestination(GetRandomLocation());
    }

    /// <summary>
    /// Handles the enemy wandering around the world.
    /// </summary>
    public void UpdateState()
    {
        if (enemy.target != null && enemy.IsPlayerInSight())
        {
            enemy.SetState(enemy.chaseState);
        }
        else if (!enemy.navMeshAgent.hasPath || enemy.navMeshAgent.remainingDistance < 0.5f)
        {
            enemy.navMeshAgent.SetDestination(GetRandomLocation());
        }
    }

    public void ExitState()
    {
        enemy.navMeshAgent.ResetPath();
    }

    /// <summary>
    /// Finds a random location around the enemy to roam to
    /// </summary>
    /// <returns>position to wander to</returns>
    private Vector3 GetRandomLocation()
    {
        return enemy.transform.position + new Vector3(Random.Range(-enemy.wanderRadius, enemy.wanderRadius), 0f, Random.Range(-enemy.wanderRadius, enemy.wanderRadius));
    }
}
