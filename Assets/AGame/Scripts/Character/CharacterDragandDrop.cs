using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDragandDrop : MonoBehaviour
{
    Vector3 offset;
    float initialY;
    private Tile currentTile = null;

    private void Start()
    {
        initialY = transform.position.y;
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDown()
    {
        offset = GetMousePos() - transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 mouseWorldPos = GetMousePos();
        transform.position = new Vector3(mouseWorldPos.x - offset.x, initialY, mouseWorldPos.z - offset.z);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            Tile hitTile = hit.collider.GetComponent<Tile>();
            if (hitTile != null)
            {
                if (currentTile != null && currentTile != hitTile)
                {
                    currentTile.ResetColor();
                }
                currentTile = hitTile;
                currentTile.Highlight();
            }
        }
        else if (currentTile != null)
        {
            currentTile.ResetColor();
            currentTile = null;
        }
    }

    private void OnMouseUp()
    {
        if (currentTile != null)
        {
            currentTile.ResetColor();
            currentTile = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Warrior") || other.CompareTag("Archer"))
        {
            Character otherCharacter = other.GetComponent<Character>();
            Character thisCharacter = GetComponent<Character>();

            if (otherCharacter.GetCharType() == thisCharacter.GetCharType() && otherCharacter.GetLevel() == thisCharacter.GetLevel())
            {
                Debug.Log("Merge");

                Tile tile = FindObjectOfType<Tile>();

                GameObject newCharacterPrefab = tile.GetCharacterPrefab(thisCharacter.GetCharType(), thisCharacter.GetLevel());

                if (newCharacterPrefab != null)
                {
                    Destroy(other.gameObject);
                    Destroy(this.gameObject);
                    Instantiate(newCharacterPrefab, transform.position, Quaternion.identity);
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
