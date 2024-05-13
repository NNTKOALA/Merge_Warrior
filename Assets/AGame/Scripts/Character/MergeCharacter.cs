using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeCharacter : MonoBehaviour
{
    public MergeManager mergeManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Warrior") || other.CompareTag("Archer"))
        {
            Character otherCharacter = other.GetComponent<Character>();
            Character thisCharacter = GetComponent<Character>();

            if (otherCharacter.GetLevel() == thisCharacter.GetLevel() && otherCharacter.GetCharType() == thisCharacter.GetCharType())
            {
                Debug.Log("Merge");

                Destroy(other.gameObject);
                Destroy(gameObject);

                GameObject newCharacterPrefab = mergeManager.GetCharacterPrefab(thisCharacter.GetCharType());
                Instantiate(newCharacterPrefab, transform.position, Quaternion.identity);
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
}
