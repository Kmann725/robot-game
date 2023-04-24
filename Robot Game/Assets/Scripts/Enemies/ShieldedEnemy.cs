using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldedEnemy : Enemy
{
    public GameObject shieldObject;

    protected override void Awake()
    {
        shieldObject.SetActive(false);
        god = true;
        base.Awake();
    }

    public override void StartAttackCoroutine()
    {
        shieldObject.SetActive(true);
        base.StartAttackCoroutine();
    }

    public override void StopAttackCoroutine()
    {
        shieldObject.SetActive(false);
        god = false;
        base.StopAttackCoroutine();
    }
}
