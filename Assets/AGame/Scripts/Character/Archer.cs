using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Character
{
    [SerializeField] Rigidbody rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
        int currentHealth = GetHealth();

        health = maxHealth;
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
        Debug.Log("Player health " + health + " max health " + maxHealth + " current health " + currentHealth);
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
