using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDragandDrop : MonoBehaviour
{
    Vector3 offset;
    float initialY;
    private PlayerTile currentTile = null;

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
        if (gameObject.tag == "Enemy")
            return;

        offset = GetMousePos() - transform.position;
        currentTile = GetComponent<PlayerTile>();
    }

    private void OnMouseDrag()
    {
        if (gameObject.tag == "Enemy")
            return;

        Vector3 mouseWorldPos = GetMousePos();
        transform.position = new Vector3(mouseWorldPos.x - offset.x, initialY, mouseWorldPos.z - offset.z);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            PlayerTile hitTile = hit.collider.GetComponent<PlayerTile>();
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
        if (gameObject.tag == "Enemy")
            return;

        if (currentTile != null)
        {
            currentTile.ResetColor();
        }
    }
}