using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyManager : MonoBehaviour
{
    public GameObject warriorPrefab;
    public GameObject archerPrefab;

    public Transform[] spawnPoints;

    public bool[] isCellOccupied;

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

    public void SpawnWarriorUnit(int cellIndex)
    {
        if (isBattleStarted) return;

        if (cellIndex < 0 || cellIndex >= spawnPoints.Length)
        {
            Debug.LogError("Invalid cell index");
            return;
        }

        if (isCellOccupied[cellIndex])
        {
            Debug.LogError("Cell is already occupied");
            return;
        }

        Debug.Log("Create Warrior Lv1");
        Instantiate(warriorPrefab, spawnPoints[cellIndex].position, spawnPoints[cellIndex].rotation);
        isCellOccupied[cellIndex] = true;

        UpdateBuyButtons();
    }

    public void SpawnArcherUnit(int cellIndex)
    {
        if (isBattleStarted) return;

        if (cellIndex < 0 || cellIndex >= spawnPoints.Length)
        {
            Debug.LogError("Invalid cell index");
            return;
        }

        if (isCellOccupied[cellIndex])
        {
            Debug.LogError("Cell is already occupied");
            return;
        }

        Debug.Log("Create Archer Lv1");
        Instantiate(archerPrefab, spawnPoints[cellIndex].position, spawnPoints[cellIndex].rotation);
        isCellOccupied[cellIndex] = true;

        UpdateBuyButtons();
    }

    public void UpdateBuyButtons()
    {
        if (isBattleStarted) return;

        bool anyCellFree = false;
        foreach (bool occupied in isCellOccupied)
        {
            if (!occupied)
            {
                anyCellFree = true;
                break;
            }
        }

        buyWarriorButton.interactable = anyCellFree;
        buyArcherButton.interactable = anyCellFree;
    }
}
