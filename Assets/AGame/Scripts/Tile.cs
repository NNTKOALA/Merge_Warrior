using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [System.Serializable]
    public class CharacterPrefabData
    {
        public string characterName;
        public CharLevel characterLevel;
        public CharType characterType;
        public GameObject characterPrefab;
    }

    public List<CharacterPrefabData> characterPrefabs = new List<CharacterPrefabData>();

    public bool IsPlaceable
    {
        get
        {
            return isPlaceable;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetCharacterPrefab(CharType type, CharLevel level)
    {
        foreach (CharacterPrefabData characterPrefabData in characterPrefabs)
        {
            if (characterPrefabData.characterType == type && characterPrefabData.characterLevel == level + 1)
            {
                return characterPrefabData.characterPrefab;
            }
        }
        return null;
    }
}
