using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemy : Enemy
{
    public LineRenderer lineRenderer;

    protected override void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        if(currentState == attackState)
        {
            lineRenderer.SetPosition(0, muzzleFlash.transform.position);
            lineRenderer.SetPosition(1, target.transform.position);
        }
    }

    protected override IEnumerator AttackCoroutine()
    {
        while(target != null)
        {
            canAttack = false;
            yield return new WaitForSeconds(attackRate);
            Attack();
            canAttack = true;
        }
    }

    public override void Attack()
    {
        enemyAnimator.Play("Shoot_SingleShot_AR");
        muzzleFlash.Play();
        GameObject bullet = Instantiate(bulletPrefab, muzzleFlash.transform.position, transform.rotation);
        Vector3 direction = new Vector3(target.transform.position.x - muzzleFlash.transform.position.x, 0, target.transform.position.z - muzzleFlash.transform.position.z).normalized;
        bullet.GetComponent<Rigidbody>().velocity = direction * 50;
        bullet.GetComponent<Bullet>().damage = attackDamage;
    }
}
