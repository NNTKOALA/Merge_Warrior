using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TakeDamage(5);
        }
    }
}
