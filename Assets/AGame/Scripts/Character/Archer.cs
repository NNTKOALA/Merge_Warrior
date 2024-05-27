using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class Archer : Character
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

        FindClosestTarget();
        if (target != null && isTargetWithinRange)
        {
            LookAtTarget();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TakeDamage(5);
        }
    }

    protected override void Attack()
    {
        base.Attack();

        ProcessRayCast();
    }

    private void ProcessRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(rb.transform.position, rb.transform.forward, out hit, chaseRange))
        {
            Character target = hit.transform.GetComponent<Character>();
            if (target == null)
            {
                return;
            }
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }
}
