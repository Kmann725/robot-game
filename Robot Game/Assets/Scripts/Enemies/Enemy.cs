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

    public float walkSpeed;
    public float runSpeed;

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

    public Animator enemyAnimator;

    public LayerMask raycastLayers;

    protected override void Awake()
    {
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
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

        Collider[] cols = Physics.OverlapSphere(transform.position, chaseRadius);
        foreach (Collider col in cols)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                target = col.gameObject;
            }
        }
    }

    public void StartAttackCoroutine()
    {
        if (attackRoutine != null)
            StopCoroutine(attackRoutine);
        attackRoutine = StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        while(target != null)
        {
            canAttack = false;
            Attack();
            yield return new WaitForSeconds(attackRate);
            canAttack = true;
        }
    }

    public void Attack()
    {
        enemyAnimator.Play("Shoot_SingleShot_AR");
        //GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
        //bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);
        //Destroy(bullet, 2f);
    }

    public void StopAttackCoroutine()
    {
        if(attackRoutine != null)
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
        if (target == null)
            return false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, target.transform.position - transform.position, out hit, 10000, raycastLayers))
        {
            if (hit.collider.CompareTag("Player"))
                return true;
        }
        if (Vector3.Distance(target.transform.position, transform.position) > chaseRadius)
            target = null;
        return false;
    }

    protected override void Destruction()
    {
        SetState(deadState);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("emp wave"))
        {
            Debug.Log("this works");
            SetState(shockState);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            if (!IsPlayerInSight())
                target = null;
    }
}
