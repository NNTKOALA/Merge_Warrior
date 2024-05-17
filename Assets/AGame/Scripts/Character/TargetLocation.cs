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
        
    }

    // Update is called once per frame
    void Update()
    {
    }


}
