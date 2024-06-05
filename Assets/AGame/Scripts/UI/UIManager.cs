using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject settingUI;
    [SerializeField] GameObject listCharacterUI;
    [SerializeField] GameObject winUI;
    [SerializeField] GameObject loseUI;
    [SerializeField] GameObject warriorCardsUI;
    [SerializeField] GameObject archerCardsUI;
    [SerializeField] TextMeshProUGUI moneyCount;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
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

    public void DeactiveAll()
    {
        inGameUI.SetActive(false);
        mainMenuUI.SetActive(false);
        settingUI.SetActive(false);
        listCharacterUI.SetActive(false);
        winUI.SetActive(false);
        loseUI.SetActive(false);
        /*warriorCardsUI.SetActive(false);
        archerCardsUI.SetActive(false);*/
    }

    public void UpdateMoney()
    {
        moneyCount.text = GameManager.Instance.FormatMoney(GameManager.Instance.playerMoney);
    }

    public void SwitchTo(GameObject ui)
    {
        DeactiveAll();
        ui.gameObject.SetActive(true);
    }

    public void SwitchToMainMenuUI()
    {
        SwitchTo(mainMenuUI);
        //UpdateMoney(ShopManager.Instance.Money);
    }

    public void SwitchToIngameUI()
    {
        SwitchTo(inGameUI);
    }

    public void SwitchToSettingUI()
    {
        SwitchTo(settingUI);
    }

    public void SwitchToListUI()
    {
        SwitchTo(listCharacterUI);
    }

    public void SwitchToWinUI()
    {
        SwitchTo(winUI);
    }

    public void SwitchToLoseUI()
    {
        SwitchTo(loseUI);
    }

    public void SwitchToWarriorCardUI()
    {
        SwitchTo(warriorCardsUI);
    }

    public void SwitchToArcherCardUI()
    {
        SwitchTo(archerCardsUI);
    }
}
