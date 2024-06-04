using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Warrior : Character
{
    [SerializeField] Rigidbody rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (!isAttack)
        {
            return;
        }

        if (target == null && isAttack == false)
        {
            FindClosestTarget();
        }
        else
        {
            MoveToTarget();
            LookAtTarget();

            if (Time.time - lastAttackTime >= attackCooldown)
            {
                OnAttack();
                lastAttackTime = Time.time;
            }
        }
    }

    protected override void OnAttack()
    {
        base.OnAttack();       
        AttackHitEvent();
    }

    private void AttackHitEvent()
    {
        if (target == null)
        {
            return;
        }
        target.GetComponent<Character>().TakeDamage(damage);
    }
}