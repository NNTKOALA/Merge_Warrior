using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTile : MonoBehaviour
{
    [SerializeField] bool isPlaceable;

    [System.Serializable]
    public struct TileCharacterData
    {
        public CharLevel characterLevel;
        public CharType characterType;
    }

    public TileCharacterData characterData;
    private GameObject currentCharacter = null;
    public Transform CurrentCharacterTF
    {
        get
        {
            if (currentCharacter == null) return null;

            return currentCharacter.transform;
        }
    }

    private Renderer renderer;
    private Color originalColor;

    public bool IsPlaceable
    {
        get
        {
            return characterData.characterType == CharType.None;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;

        if (characterData.characterType != CharType.None)
        {
            GameObject prefab = CharacterData.Instance.GetCharacterPrefab(characterData.characterType, characterData.characterLevel);
            currentCharacter = Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

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

    public void ResetCharacterPosition()
    {
        currentCharacter.transform.position = transform.position;
    }

    public void ClearData()
    {
        characterData.characterType = CharType.None;
    }

    public void UpdateCharacter()
    {
        if (currentCharacter != null)
        {
            Destroy(currentCharacter);
        }

        currentCharacter = null;

        if (characterData.characterType == CharType.None)
        {
            return;
        }

        GameObject prefab = CharacterData.Instance.GetCharacterPrefab(characterData.characterType, characterData.characterLevel);

        currentCharacter = Instantiate(prefab, transform.position, Quaternion.identity);

        Character characterScript = currentCharacter.GetComponent<Character>();
        if (characterScript != null)
        {
            characterScript.GetHealth();
        }
    }

    public void SpawnCharacterWithHealth()
    {
        if (characterData.characterType != CharType.None)
        {
            if (currentCharacter != null)
            {
                Destroy(currentCharacter);
            }

            GameObject prefab = CharacterData.Instance.GetCharacterPrefab(characterData.characterType, characterData.characterLevel);
            currentCharacter = Instantiate(prefab, transform.position, Quaternion.identity);
            
            Character characterScript = currentCharacter.GetComponent<Character>();
            if (characterScript != null)
            {
                characterScript.GetHealth();
            }
        }
    }
}

