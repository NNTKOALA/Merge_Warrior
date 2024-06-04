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

        if (!isAttack)
        {
            return;
        }

        if (target == null)
        {
            FindClosestTarget();
        }
        else
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

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, chaseRange, characterLayer))
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
}
