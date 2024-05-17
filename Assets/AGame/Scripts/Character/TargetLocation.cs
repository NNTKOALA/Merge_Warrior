using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.AI;

public class TargetLocation : MonoBehaviour
{
    public float turnSpeed = 5f;
    public float chaseRange = 10f;

    private NavMeshAgent navMeshAgent;
    private Transform target;
    private bool isTargetWithinRange;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        if (target != null && isTargetWithinRange)
        {
            MoveToTarget();
            LookAtTarget();
        }
    }

    void FindClosestTarget()
    {
        EnemyTile[] enemies = FindObjectsOfType<EnemyTile>();
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (EnemyTile enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestTarget = enemy.transform;
            }
        }

        target = closestTarget;
        isTargetWithinRange = (target != null && closestDistance <= chaseRange);
    }

    void MoveToTarget()
    {
        if (target != null)
        {
            navMeshAgent.SetDestination(target.position);
        }
    }

    void LookAtTarget()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
