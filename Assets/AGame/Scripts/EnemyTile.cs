using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTile : MonoBehaviour
{
    private HealthBar healthBar;

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
            currentCharacter.tag = "Enemy";
        }
    }
}
