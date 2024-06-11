using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Warrior : Character
{
    [SerializeField] Rigidbody rb;

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

        if (target != null)
        {
            MoveToTarget();

            if (IsTargetInRange() && Time.time - lastAttackTime >= attackCooldown)
            {
                //LookAtTarget(target.position);
                OnAttack();
                lastAttackTime = Time.time;
            }
        }
    }

    private void MoveToTarget()
    {
        if (target != null && !IsTargetInRange())
        {
            anim.SetBool("isMoving", true);
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    private bool IsTargetInRange()
    {
        if (target == null)
            return false;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        return distanceToTarget <= attackRange;
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