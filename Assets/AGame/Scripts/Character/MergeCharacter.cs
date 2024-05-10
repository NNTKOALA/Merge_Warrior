using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeCharacter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Warrior") || other.gameObject.CompareTag("Archer"))
        {
            Character otherCharacter = other.GetComponent<Character>();
            Character thisCharacter = GetComponent<Character>();

            if (otherCharacter.GetLevel() == thisCharacter.GetLevel() && otherCharacter.GetCharType() == thisCharacter.GetCharType())
            {
                Debug.Log("Merger");
                Destroy(gameObject);
                CreateNewCharacter(thisCharacter.GetLevel() + 1);
            }
            else
            {
                Debug.Log("Change Position");
                Vector3 temp = other.transform.position;
                other.transform.position = transform.position;
                transform.position = temp;
            }
        }
    }

    private void CreateNewCharacter(int newLevel)
    {
        //GameObject newCharacter = Instantiate(CharacterPrefab, transform.position, Quaternion.identity);
        Debug.Log("New Character");
    }
}
