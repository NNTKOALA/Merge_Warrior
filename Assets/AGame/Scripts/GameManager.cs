using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public Button fightButton;
    public bool isFighting;

    public float playerMoney = 200;
    public bool isPlaying;

    public TextMeshProUGUI levelText;
    public int currentLevelIndex = 1;

    [SerializeField] private GameObject characterCardPrefab;
    private GameObject characterCardInstance;

    public LevelManager levelManager;

    public TextMeshProUGUI winRewardText;
    public TextMeshProUGUI loseRewardText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (fightButton != null)
        {
            fightButton.onClick.AddListener(StartBattle);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isFighting = false;
        PauseGame();
        UIManager.Instance.UpdateMoney();
        UpdateLevelText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            WinGame();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoseGame();
        }
        CheckGameStatus();
    }

    public void WinGame()
    {
        AudioManager.Instance.PlaySFX("Win");
        PauseGame();
        int totalPlayerSquadDamage = CalculateTotalPlayerSquadDamage();
        int moneyEarned = totalPlayerSquadDamage * 10;
        AddMoney(moneyEarned);
        UIManager.Instance.SwitchToWinUI();
        winRewardText.text = $"Reward: + {moneyEarned} ";
    }

    public void LoseGame()
    {
        AudioManager.Instance.PlaySFX("Lose");
        PauseGame();
        int totalEnemySquadHP = CalculateTotalEnemySquadHP();
        AddMoney(totalEnemySquadHP);
        UIManager.Instance.SwitchToLoseUI();
        loseRewardText.text = $"Reward: + {totalEnemySquadHP} ";
    }

    private int CalculateTotalPlayerSquadDamage()
    {
        int totalDamage = 0;
        Character[] characters = FindObjectsOfType<Character>();
        foreach (Character character in characters)
        {
            if (!character.isEnemy && !character.isDead)
            {
                totalDamage += character.damage;
            }
        }
        return totalDamage;
    }

    private int CalculateTotalEnemySquadHP()
    {
        int totalHP = 0;
        Character[] characters = FindObjectsOfType<Character>();
        foreach (Character character in characters)
        {
            if (character.isEnemy && character.isDead)
            {
                totalHP += character.maxHealth - character.health;
            }
        }
        return totalHP;
    }

    public void PauseGame()
    {
        isPlaying = false;
    }

    public void ResumeGame()
    {
        isPlaying = true;
    }

    public void CheckGameStatus()
    {
        int playerCount = 0;
        int enemyCount = 0;
        int alivePlayerCount = 0;
        int aliveEnemyCount = 0;

        Character[] characters = FindObjectsOfType<Character>();
        foreach (Character character in characters)
        {
            if (character.isEnemy)
            {
                enemyCount++;
                if (!character.isDead)
                {
                    aliveEnemyCount++;
                }
            }
            else
            {
                playerCount++;
                if (!character.isDead)
                {
                    alivePlayerCount++;
                }
            }
        }

        if (alivePlayerCount == 0 && playerCount > 0)
        {
            LoseGame();
        }
        else if (aliveEnemyCount == 0 && enemyCount > 0)
        {
            WinGame();
        }
    }


    public bool HasEnoughMoney(float amount)
    {
        return playerMoney >= amount;
    }

    public void DeductMoney(float amount)
    {
        if (HasEnoughMoney(amount))
        {
            playerMoney -= amount;
            UIManager.Instance.UpdateMoney();
            Debug.Log("Bank balace" + playerMoney);
        }
    }

    public void AddMoney(float amount)
    {
        playerMoney += amount;
        UIManager.Instance.UpdateMoney();
    }

    public string FormatMoney(float amount)
    {
        if (amount < 1000)
        {
            return Mathf.Round(amount).ToString("F0");
        }
        else if (1000 <= amount && amount < 1000000)
        {
            float value = Mathf.Round(amount / 100f) / 10;
            return value.ToString(value % 1 == 0 ? "F0" : "F1") + "K";
        }
        else
        {
            float value = Mathf.Round(amount / 100000f) / 10;
            return value.ToString(value % 1 == 0 ? "F0" : "F1") + "M";
        }
    }

    public void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = $"Level {currentLevelIndex}";
        }
        else
        {
            Debug.LogError("levelText is not assigned in the GameManager.");
        }
    }

    public void DisplayCharacterCard(string name, int attack, int health)
    {
        characterCardInstance = Instantiate(characterCardPrefab, Vector3.zero, Quaternion.identity);
        characterCardInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);

        CharacterCard characterCardUI = characterCardInstance.GetComponent<CharacterCard>();
        if (characterCardUI != null)
        {
            characterCardUI.UpdateCharacterData(name, attack, health);
        }
    }

    public void StartBattle()
    {
        isFighting = true;
        Character[] characters = FindObjectsOfType<Character>();
        foreach (Character character in characters)
        {
            character.StartBattle();
        }
    }

    public void ResetBattle()
    {
        isFighting = false;
        Character[] characters = FindObjectsOfType<Character>();
        foreach (Character character in characters)
        {
            character.ResetBattle();
        }
    }
}
