using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeCharater : MonoBehaviour
{
    public Collider allowedArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Character otherCharacter = other.GetComponent<Character>();
            Character thisCharacter = GetComponent<Character>();

            if (otherCharacter != null && thisCharacter != null)
            {
                if (IsOnPlayerTile(thisCharacter) && IsOnPlayerTile(otherCharacter))
                {
                    if (otherCharacter.GetCharType() == thisCharacter.GetCharType() &&
                        otherCharacter.GetLevel() == thisCharacter.GetLevel())
                    {
                        Debug.Log("Merge");

                        PlayerTile tile = FindObjectOfType<PlayerTile>();

                        if (tile != null)
                        {
                            GameObject newCharacterPrefab = tile.GetCharacterPrefab(thisCharacter.GetCharType(), thisCharacter.GetLevel() + 1);

                            if (newCharacterPrefab != null)
                            {
                                Destroy(other.gameObject);
                                Destroy(this.gameObject);
                                Instantiate(newCharacterPrefab, transform.position, Quaternion.identity);
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Change Position");

                        Vector3 tempPosition = other.transform.position;
                        other.transform.position = this.transform.position;
                        this.transform.position = tempPosition;
                    }
                }
            }
        }
    }

    private bool IsOnPlayerTile(Character character)
    {
        RaycastHit hit;
        if (Physics.Raycast(character.transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.collider != null && hit.collider.GetComponent<PlayerTile>() != null)
            {
                return true;
            }
        }
        return false;
    }
}