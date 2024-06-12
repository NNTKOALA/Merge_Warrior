using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class Archer : Character
{
    [SerializeField] Rigidbody rb;
    public LayerMask targetLayer;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected override void Update()
    {
        base.Update();
        if (GameManager.Instance.isFighting)
        {
            ProcessFight();
        }
    }

    private void ProcessFight()
    {
        if (target == null)
        {
            FindClosestEnemy();
        }
        else
        {
            //LookAtTarget(target.position);

            if (Time.time - lastAttackTime >= attackCooldown)
            {
                AttackHitEvent();
                lastAttackTime = Time.time;
            }
        }
    }

    private void AttackHitEvent()
    {
        if (target == null)
        {
            return;
        }

        if (target != null && target.tag != this.tag)
        {
            OnAttack();
            target.GetComponent<Character>().TakeDamage(damage);
        }
    }

    protected override void OnAttack()
    {
        if (target != null)
        {
            Character enemy = target.GetComponent<Character>();
            if (enemy != null)
            {
                ChangeAnim("attack");
                enemy.TakeDamage(damage);
            }
        }
    }

    public override void OnNewGame()
    {
        base.OnNewGame();
        isDead = false;
    }
}
