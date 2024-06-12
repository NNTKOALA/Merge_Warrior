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
    private int purchaseCount = 0;

    private Color normalColor = Color.white;
    private Color disabledColor = Color.gray;

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
        if (cellIndex < 0 || !GameManager.Instance.HasEnoughMoney(currentPrice)) return;

        GameManager.Instance.DeductMoney(currentPrice);
        Debug.Log($"Purchased Warrior for {currentPrice} units.");

        playerTileList[cellIndex].characterData.characterType = CharType.Warrior;
        playerTileList[cellIndex].characterData.characterLevel = CharLevel.Lv1;
        playerTileList[cellIndex].UpdateCharacter();
    }

    public void SpawnArcherUnit()
    {
        cellIndex = FindAvailableIndex();
        if (cellIndex < 0 || !GameManager.Instance.HasEnoughMoney(currentPrice)) return;

        GameManager.Instance.DeductMoney(currentPrice);
        Debug.Log($"Purchased Archer for {currentPrice} units.");

        playerTileList[cellIndex].characterData.characterType = CharType.Archer;
        playerTileList[cellIndex].characterData.characterLevel = CharLevel.Lv1;
        playerTileList[cellIndex].UpdateCharacter();
    }

    public void UpdateBuyButtons()
    {
        if (isBattleStarted)
        {
            return;
        }

        bool hasAvailableIndex = FindAvailableIndex() >= 0;

        float warriorPrice = currentPrice;
        float archerPrice = currentPrice;

        bool hasEnoughMoneyForWarrior = GameManager.Instance.HasEnoughMoney(warriorPrice);
        bool hasEnoughMoneyForArcher = GameManager.Instance.HasEnoughMoney(archerPrice);
    }

    public int FindAvailableIndex()
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

    public void UpdatePrice()
    {
        if (purchaseCount == 0)
        {
            currentPrice = 0;
        }
        else if (purchaseCount == 1)
        {
            currentPrice = 120;
        }
        else if (purchaseCount == 2)
        {
            currentPrice = 120 * priceIncreaseRate;
        }
        else
        {
            currentPrice *= priceIncreaseRate;
        }
        UpdatePriceText();
    }

    public void BuyCharacter()
    {
        if (GameManager.Instance.HasEnoughMoney(currentPrice))
        {
            purchaseCount++;
            UpdatePrice();
            UpdateBuyButtons();
            AudioManager.Instance.PlaySFX("Buy");
        }
    }
}
