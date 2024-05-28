using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    [SerializeField] private LayerMask tileLayer;
    private Camera mainCam;

    private PlayerTile startTile;
    private Transform characterTF = null;

    private void Start()
    {
        mainCam = Camera.main;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, tileLayer))
            {
                startTile = hit.collider.GetComponent<PlayerTile>();
                if (startTile != null)
                {
                    characterTF = startTile.CurrentCharacterTF;
                }
                else
                {
                    Debug.LogWarning("Raycast did not hit a PlayerTile");
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (characterTF == null) return;

            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, tileLayer))
            {
                characterTF.position = hit.point;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (startTile == null)
            {
                Debug.LogWarning("startTile is null on MouseButtonUp");
                return;
            }

            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, tileLayer))
            {
                PlayerTile endTile = hit.collider.GetComponent<PlayerTile>();
                CheckMerge(startTile, endTile);
                startTile = null;
                characterTF = null;
            }
            else
            {
                startTile.ResetCharacterPosition();
                startTile = null;
                characterTF = null;
            }
        }
    }

    private void CheckMerge(PlayerTile firstTile, PlayerTile secondTile)
    {
        if (firstTile == null)
        {
            Debug.LogError("firstTile is null in CheckMerge");
            return;
        }

        if (secondTile == null)
        {
            Debug.Log("secondTile is null, reset firstTile");
            firstTile.ResetCharacterPosition();
            return;
        }

        if (firstTile == secondTile) return;

        if (firstTile.characterData.characterType == CharType.None)
        {
            return;
        }

        if (secondTile.characterData.characterType == CharType.None)
        {
            secondTile.characterData = firstTile.characterData;
            firstTile.ClearData();

            firstTile.UpdateCharacter();
            secondTile.UpdateCharacter();

            return;
        }

        if (firstTile.characterData.characterType == secondTile.characterData.characterType &&
            firstTile.characterData.characterLevel == secondTile.characterData.characterLevel)
        {
            firstTile.ClearData();
            secondTile.characterData.characterLevel++;

            firstTile.UpdateCharacter();
            secondTile.UpdateCharacter();
        }
        else
        {
            firstTile.ResetCharacterPosition();
        }
    }
}
