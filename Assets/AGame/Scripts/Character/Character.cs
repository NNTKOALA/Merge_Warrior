using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float health = 0f;
    [SerializeField] float damage = 0f;
    [SerializeField] float range = 0f;
    [SerializeField] float level = 0f;
    [SerializeField] CharType charType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Die");
        }
    }

    public void MergeCharacter()
    {

    }
}
