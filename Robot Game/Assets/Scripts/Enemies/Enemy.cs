using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Damageable
{
    public float wanderRadius;
    public float chaseRadius;
    public float attackRadius;

    public float attackRate;
    public float attackDamage;

    public float speed;

    public IEnemyState wanderState;
    public IEnemyState chaseState;
    public IEnemyState attackState;
    public IEnemyState shockState;
    public IEnemyState deadState;

    public GameObject target;

    public GameObject bulletPrefab;

    public NavMeshAgent navMeshAgent;

    protected IEnemyState currentState;

    protected Coroutine attackRoutine;

    protected bool canAttack = true;

    protected override void Awake()
    {
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        wanderState = new WanderState(this);
        chaseState = new ChaseState(this);
        attackState = new AttackState(this);
        shockState = new DeadState(this);
        deadState = new DeadState(this);
        SetState(wanderState);
        // Create state objects
    }

    private void Update()
    {
        if (currentState != null)
            currentState.UpdateState();
    }

    public void StartAttackCoroutine()
    {
        if (attackRoutine != null)
            StopCoroutine(attackRoutine);
        attackRoutine = StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        canAttack = false;
        Attack();
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }

    public void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);
        Destroy(bullet, 2f);
    }

    public void StopAttackCoroutine()
    {
        StopCoroutine(attackRoutine);
    }

    public virtual void SetState(IEnemyState state)
    {
        if(currentState != null)
            currentState.ExitState();
        currentState = state;
        if (currentState != null)
            currentState.EnterState();
    }

    public bool IsPlayerInSight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, target.transform.position - transform.position, out hit))
        {
            if (hit.collider.CompareTag("Player"))
                return true;
        }
        return false;
    }

    protected override void Destruction()
    {
        SetState(deadState);
    }
}
