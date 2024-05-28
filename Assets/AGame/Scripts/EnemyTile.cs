using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.tag = "Enemy";
            Character character = other.GetComponent<Character>();
            if (character != null)
            {
                // character.UpdateHealthBarColor(Color.red);
            }
        }
    }
}
