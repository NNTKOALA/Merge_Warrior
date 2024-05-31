using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyManager : MonoBehaviour
{
    public GameObject tilePrefab;

    public List<PlayerTile> playerTileList;

    private int cellIndex;

    bool isBattleStarted = false;

    [SerializeField] Button buyWarriorButton;
    [SerializeField] Button buyArcherButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnWarriorUnit()
    {
        cellIndex = FindAvailableIndex();
        if (cellIndex < 0) return;

        playerTileList[cellIndex].characterData.characterType = CharType.Warrior;
        playerTileList[cellIndex].characterData.characterLevel = CharLevel.Lv1;
        playerTileList[cellIndex].UpdateCharacter();
        // UpdateBuyButtons();
    }

    public void SpawnArcherUnit()
    {
        cellIndex = FindAvailableIndex();
        if (cellIndex < 0) return;

        playerTileList[cellIndex].characterData.characterType = CharType.Archer;
        playerTileList[cellIndex].characterData.characterLevel = CharLevel.Lv1;
        playerTileList[cellIndex].UpdateCharacter();
        // UpdateBuyButtons();
    }

    public void UpdateBuyButtons()
    {
        if (isBattleStarted) return;
    }

    private int FindAvailableIndex()
    {
        for (int i = 0; i < playerTileList.Count; i++)
        {
            if (playerTileList[i].characterData.characterType == CharType.None)
            {
                return i;
            }
        }

        return -1;
    }
}
