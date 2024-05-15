using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Warrior") || other.CompareTag("Archer"))
        {
            Character otherCharacter = other.GetComponent<Character>();
            Character thisCharacter = GetComponent<Character>();

            if (otherCharacter.GetCharType() == thisCharacter.GetCharType() && otherCharacter.GetLevel() == thisCharacter.GetLevel())
            {
                Debug.Log("Merge");

                Tile tile = FindObjectOfType<Tile>();
                GameObject newCharacterPrefab = tile.GetCharacterPrefab(thisCharacter.GetCharType(), thisCharacter.GetLevel() + 1);

                if (newCharacterPrefab != null)
                {
                    Instantiate(newCharacterPrefab, transform.position, Quaternion.identity);
                }

                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("Change Position");

                Vector3 tempPosition = other.transform.position;
                other.transform.position = transform.position;
                transform.position = tempPosition;
            }
        }
    }
}
