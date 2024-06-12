using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    [SerializeField] GameObject gameMenuUI;
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject moneybarUI;
    [SerializeField] GameObject settingUI;
    [SerializeField] GameObject charCardUI;
    [SerializeField] GameObject listCharacterUI;
    [SerializeField] GameObject warriorListCardsUI;
    [SerializeField] GameObject archerListCardsUI;
    [SerializeField] GameObject winUI;
    [SerializeField] GameObject loseUI;
    [SerializeField] TextMeshProUGUI moneyCount;
    [SerializeField] GameObject map;

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
        gameMenuUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame()
    {
        GameManager.Instance.OnNewGame();
    }

    public void DeactiveAll()
    {
        mainMenuUI.SetActive(false);
        settingUI.SetActive(false);
        charCardUI.SetActive(false);
        listCharacterUI.SetActive(false);
        warriorListCardsUI.SetActive(false);
        archerListCardsUI.SetActive(false);
        winUI.SetActive(false);
        loseUI.SetActive(false);
        map.SetActive(false);
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
        gameMenuUI.SetActive(false);
        map.SetActive(true);
        moneybarUI.SetActive(true);
    }

    public void SwitchToIngameUI()
    {
        SwitchTo(moneybarUI);
        map.SetActive(true);
    }

    public void SwitchToSettingUI()
    {
        mainMenuUI.SetActive(true);
        settingUI.SetActive(true);
    }

    public void SwitchToListUI()
    {
        mainMenuUI.SetActive(true) ;
        listCharacterUI.SetActive(true);
        warriorListCardsUI.SetActive(true);
        archerListCardsUI.SetActive(false);
    }

    public void SwitchToWinUI()
    {
        winUI.SetActive(true);
    }

    public void SwitchToLoseUI()
    {
        loseUI.SetActive(true);
    }

    public void SwitchToWarriorCardUI()
    {
        mainMenuUI.SetActive(true);
        listCharacterUI.SetActive(true);
        warriorListCardsUI.SetActive(true);
        archerListCardsUI.SetActive(false);
    }

    public void SwitchToArcherCardUI()
    {
        mainMenuUI.SetActive(true);
        listCharacterUI.SetActive(true);
        warriorListCardsUI.SetActive(false);
        archerListCardsUI.SetActive(true);
    }

    public void ShowCharCardUI()
    {
        charCardUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
