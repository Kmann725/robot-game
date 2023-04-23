using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : Damageable
{
    public float wanderRadius = 15;
    public float chaseRadius = 20;
    public float attackRadius = 10;

    public float attackRate = 1;
    public int attackDamage = 5;

    public float walkSpeed = 2;
    public float runSpeed = 5;

    public IEnemyState wanderState;
    public IEnemyState chaseState;
    public IEnemyState attackState;
    public IEnemyState shockState;
    public IEnemyState deadState;

    public GameObject target;

    public GameObject bulletPrefab;

    [HideInInspector] public NavMeshAgent navMeshAgent;

    protected IEnemyState currentState;

    protected Coroutine attackRoutine;
    protected Coroutine shockRoutine;

    protected bool canAttack = true;

    [HideInInspector] public Animator enemyAnimator;

    public LayerMask raycastLayers;

    public ParticleSystem muzzleFlash;

    protected override void Awake()
    {
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        wanderState = new WanderState(this);
        chaseState = new ChaseState(this);
        attackState = new AttackState(this);
        shockState = new ShockState(this);
        deadState = new DeadState(this);
        SetState(wanderState);
        // Create state objects
    }

    protected virtual void Update()
    {
        if (currentState != null)
            currentState.UpdateState();
    }

    public virtual void StartAttackCoroutine()
    {
        if (attackRoutine != null)
            StopCoroutine(attackRoutine);
        attackRoutine = StartCoroutine(AttackCoroutine());
    }

    protected virtual IEnumerator AttackCoroutine()
    {
        while(target != null)
        {
            canAttack = false;
            Attack();
            yield return new WaitForSeconds(attackRate);
            canAttack = true;
        }
    }

    public virtual void StartShockCoroutine()
    {
        if(shockRoutine != null)
        {
            StopCoroutine(shockRoutine);
        }
        shockRoutine = StartCoroutine(ShockCoroutine());
    }

    protected virtual IEnumerator ShockCoroutine()
    {
        yield return new WaitForSeconds(3);
        SetState(wanderState);
    }

    public virtual void Attack()
    {
        enemyAnimator.Play("Shoot_SingleShot_AR");
        muzzleFlash.Play();
        GameObject bullet = Instantiate(bulletPrefab, muzzleFlash.transform.position, transform.rotation);
        Vector3 direction = new Vector3(target.transform.position.x - muzzleFlash.transform.position.x, 0, target.transform.position.z - muzzleFlash.transform.position.z).normalized;
        bullet.GetComponent<Rigidbody>().velocity = direction * 15;
        bullet.GetComponent<Bullet>().damage = attackDamage;
    }

    public virtual void StopAttackCoroutine()
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

    public virtual bool IsPlayerInSight()
    {
        if (target == null)
            return false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, target.transform.position - transform.position, out hit, 10000, raycastLayers))
        {
            if (hit.collider.CompareTag("Player"))
                return true;
        }
        if (Vector3.Distance(target.transform.position, transform.position) > chaseRadius + 5)
            target = null;
        return false;
    }

    protected override void Destruction()
    {
        SetState(deadState);
        GameController.Instance.EnemyKilled();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("physics object") && collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 1)
        {
            TakeDamage(30);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            target = other.gameObject;
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            if (!IsPlayerInSight())
                target = null;
    }
}
