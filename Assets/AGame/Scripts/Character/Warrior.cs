using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
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
    }

    private void ProcessFight()
    {
        if (target == null)
        {
            FindClosestEnemy();
        }

        if (target != null)
        {
            MoveToTarget();
            LookAtTarget(target.position);

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

        if (target != null && target.tag != this.tag)
        {
            target.GetComponent<Character>().TakeDamage(damage);
        }
    }

    public void MoveToTarget()
    {
        if (target != null)
        {
            anim.SetBool("isMoving", true);
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
}