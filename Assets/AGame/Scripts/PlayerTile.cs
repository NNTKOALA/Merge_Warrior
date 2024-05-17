using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTile : MonoBehaviour
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

    private Renderer renderer;
    private Color originalColor;

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
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
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

    public void Highlight()
    {
        renderer.material.color = Color.green;
    }

    public void ResetColor()
    {
        renderer.material.color = originalColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
