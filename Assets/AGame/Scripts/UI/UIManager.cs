using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject moneybarUI;
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
        moneybarUI.SetActive(false);
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
        SwitchTo(moneybarUI);
        //UpdateMoney(ShopManager.Instance.Money);
    }

    public void SwitchToIngameUI()
    {
        SwitchTo(inGameUI);
    }

    public void SwitchToSettingUI()
    {
        inGameUI.SetActive(true);
        settingUI.SetActive(true);
    }

    public void SwitchToListUI()
    {
        inGameUI.SetActive(true) ;
        listCharacterUI.SetActive(true);
    }

    public void SwitchToWinUI()
    {
        inGameUI.SetActive(true);
        winUI.SetActive(true);
    }

    public void SwitchToLoseUI()
    {
        inGameUI.SetActive(true);
        loseUI.SetActive(true);
    }

    public void SwitchToWarriorCardUI()
    {
        inGameUI.SetActive(true);
        listCharacterUI.SetActive(true);
        warriorCardsUI.SetActive(true);
    }

    public void SwitchToArcherCardUI()
    {
        inGameUI.SetActive(true);
        listCharacterUI.SetActive(true);
        archerCardsUI.SetActive(true);
    }
}
