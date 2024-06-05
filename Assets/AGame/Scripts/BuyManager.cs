using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public TextMeshProUGUI priceText;
    private float currentPrice = 0;
    private const float priceIncreaseRate = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePriceText();
        UpdateBuyButtons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnWarriorUnit()
    {
        cellIndex = FindAvailableIndex();
        if (cellIndex < 0 || !GameManager.Instance.HasEnoughMoney(currentPrice)) return;

        GameManager.Instance.DeductMoney(currentPrice);

        playerTileList[cellIndex].characterData.characterType = CharType.Warrior;
        playerTileList[cellIndex].characterData.characterLevel = CharLevel.Lv1;
        playerTileList[cellIndex].UpdateCharacter();

        UpdateBuyButtons();
        UpdatePrice();
    }

    public void SpawnArcherUnit()
    {
        cellIndex = FindAvailableIndex();
        if (cellIndex < 0 || !GameManager.Instance.HasEnoughMoney(currentPrice)) return;

        GameManager.Instance.DeductMoney(currentPrice);

        playerTileList[cellIndex].characterData.characterType = CharType.Archer;
        playerTileList[cellIndex].characterData.characterLevel = CharLevel.Lv1;
        playerTileList[cellIndex].UpdateCharacter();

        UpdateBuyButtons();
        UpdatePrice();
    }

    public void UpdateBuyButtons()
    {
        if (isBattleStarted)
        {
            buyWarriorButton.interactable = false;
            buyArcherButton.interactable = false;
            return;
        }

        bool hasAvailableIndex = FindAvailableIndex() >= 0;
        bool hasEnoughMoney = GameManager.Instance.HasEnoughMoney(currentPrice);

        buyWarriorButton.interactable = hasAvailableIndex && hasEnoughMoney;
        buyArcherButton.interactable = hasAvailableIndex && hasEnoughMoney;
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

    public void UpdatePriceText()
    {
        priceText.text = GameManager.Instance.FormatMoney(currentPrice);
    }

    private void UpdatePrice()
    {
        currentPrice = 120 + currentPrice * priceIncreaseRate;
        UpdatePriceText();
    }

    public void BuyCharacter()
    {
        if (GameManager.Instance.HasEnoughMoney(currentPrice))
        {
            GameManager.Instance.DeductMoney(currentPrice);

            UpdatePrice();
            UpdateBuyButtons();
        }
        else
        {
            Debug.Log("Not enough money to buy the character.");
        }
    }
}
