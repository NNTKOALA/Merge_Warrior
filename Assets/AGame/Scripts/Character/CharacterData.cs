using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public List<CharacterPrefabData> characterPrefabs = new List<CharacterPrefabData>();

    public static CharacterData Instance { get; private set; }
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetCharacterPrefab(CharType type, CharLevel level)
    {
        foreach (CharacterPrefabData characterPrefabData in characterPrefabs)
        {
            if (characterPrefabData.characterType == type && characterPrefabData.characterLevel == level)
            {
                return characterPrefabData.characterPrefab;
            }
        }
        return null;
    }
}
[System.Serializable]
public class CharacterPrefabData
{
    public string characterName;
    public CharLevel characterLevel;
    public CharType characterType;
    public GameObject characterPrefab;
}
