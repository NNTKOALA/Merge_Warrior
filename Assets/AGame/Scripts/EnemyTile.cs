using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTile : MonoBehaviour
{
    [System.Serializable]
    public struct TileCharacterData
    {
        public CharLevel characterLevel;
        public CharType characterType;
    }

    public TileCharacterData characterData;
    private GameObject currentCharacter = null;

    void Start()
    {
        if (characterData.characterType != CharType.None)
        {
            GameObject prefab = CharacterData.Instance.GetCharacterPrefab(characterData.characterType, characterData.characterLevel);
            currentCharacter = Instantiate(prefab, transform.position, Quaternion.Euler(0, 180, 0));
        }
    }

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
