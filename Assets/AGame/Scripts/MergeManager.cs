using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    [System.Serializable]
    public class CharacterPrefabData
    {
        public string characterName;
        public CharType characterType;
        public GameObject characterPrefab;
    }

    public List<CharacterPrefabData> characterPrefabs = new List<CharacterPrefabData>();

    public GameObject GetCharacterPrefab(CharType type)
    {
        foreach (CharacterPrefabData characterPrefabData in characterPrefabs)
        {
            if (characterPrefabData.characterType == type)
            {
                return characterPrefabData.characterPrefab;
            }
        }
        return null;
    }
}
