using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class Archer : Character
{
    [SerializeField] Rigidbody rb;

    public LayerMask targetLayer;

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

/*        if (!isAttack)
        {
            return;
        }*/

        FindClosestTarget();

        if (target != null)
        {
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
        ProcessRayCast();
    }

    private void ProcessRayCast()
    {
        if (rb == null)
        {
            return;
        }

        RaycastHit hit;
        Vector3 rayOrigin = rb.transform.position;
        Vector3 rayDirection = rb.transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, attackRange, characterLayer))
        {
            if (hit.collider.gameObject != gameObject)
            {
                Character target = hit.transform.GetComponent<Character>();
                if (target != null && target.tag != this.tag)
                {
                    target.TakeDamage(damage);
                }
            }
        }
    }

    protected override void FindClosestTarget()
    {
        Character[] enemies = FindObjectsOfType<Character>();
        Transform closestTarget = null;
        float maxDistance = attackRange;

        foreach (Character enemy in enemies)
        {
            if (enemy == this || enemy.tag == this.tag)
            {
                continue;
            }

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = distanceToEnemy;
            }
        }

        target = closestTarget;
    }

    protected override void LookAtTarget()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

            float targetDistance = Vector3.Distance(transform.position, target.position);

            if (targetDistance <= attackRange)
            {
                OnAttack();
            }
            else
            {
                OnIdle();
            }
        }
    }
}
