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

        if (target == null || !isTargetWithinRange)
        {
            FindClosestTarget();
            LookAtTarget();
        }
    }

    protected override void Attack()
    {
        base.Attack();        
        ProcessRayCast();
    }

    private void ProcessRayCast()
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody is not assigned.");
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
                    Debug.Log(name + " attacked " + target.name + " for " + damage + " damage.");
                }
            }
        }
        else
        {
            Debug.Log("No target hit within range.");
        }
    }
}
