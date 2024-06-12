using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public Button fightButton;
    public bool isFighting;

    public float playerMoney = 200;

    public TextMeshProUGUI levelText;
    public int currentLevelIndex = 1;

    public LevelManager levelManager;

    public TextMeshProUGUI winRewardText;
    public TextMeshProUGUI loseRewardText;

    [SerializeField] public Button resetDataButton;

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
        UIManager.Instance.UpdateMoney();
        UpdateLevelText();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameStatus();
    }

    public void CheckGameStatus()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        int alivePlayerCount = 0;
        foreach (GameObject playerObject in playerObjects)
        {
            Character character = playerObject.GetComponent<Character>();
            if (character != null && !character.isDead)
            {
                alivePlayerCount++;
            }
        }

        int aliveEnemyCount = 0;
        foreach (GameObject enemyObject in enemyObjects)
        {
            Character character = enemyObject.GetComponent<Character>();
            if (character != null && !character.isDead)
            {
                aliveEnemyCount++;
            }
        }

        Debug.Log($"CheckGameStatus - Alive Players: {alivePlayerCount}, Alive Enemies: {aliveEnemyCount}");

        if (alivePlayerCount == 0 && playerObjects.Length > 0)
        {
            LoseGame();
        }
        else if (aliveEnemyCount == 0 && enemyObjects.Length > 0)
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        Debug.Log("WinGame method called");
        AudioManager.Instance.PlaySFX("Win");
        int totalPlayerSquadDamage = CalculateTotalPlayerSquadDamage();
        Debug.Log($"Total player squad damage: {totalPlayerSquadDamage}");
        AddMoney(totalPlayerSquadDamage);
        winRewardText.text = $" Reward: + {totalPlayerSquadDamage * 10}";
        StartCoroutine(DisplayWinScreenAfterDelay(2f));
    }

    public void LoseGame()
    {
        Debug.Log("LoseGame method called");
        AudioManager.Instance.PlaySFX("Lose");
        int totalPlayerSquadDamage = CalculateTotalPlayerSquadDamage();
        Debug.Log($"Total player squad damage: {totalPlayerSquadDamage}");
        AddMoney(totalPlayerSquadDamage);
        loseRewardText.text = $" Reward: + {totalPlayerSquadDamage * 10}";
        StartCoroutine(DisplayLoseScreenAfterDelay(2f));
    }

    private int CalculateTotalPlayerSquadDamage()
    {
        int totalDamage = 0;
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        Debug.Log($"Number of Player Objects: {playerObjects.Length}");

        foreach (GameObject playerObject in playerObjects)
        {
            Character character = playerObject.GetComponent<Character>();
            if (character != null && !character.isEnemy)
            {
                totalDamage += character.damage;
            }
        }
        return totalDamage;
    }

    private IEnumerator DisplayWinScreenAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        UIManager.Instance.SwitchToWinUI();
    }

    private IEnumerator DisplayLoseScreenAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        UIManager.Instance.SwitchToLoseUI();
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

/*    public void ResetGameData()
    {
        Debug.Log("Reset Game Data");

        playerMoney = 800;
        currentLevelIndex = 1;
        UpdateLevelText();
        UIManager.Instance.UpdateMoney();
    }*/

    public void OnNewGame()
    {
        PlayerTile[] allTiles = FindObjectsOfType<PlayerTile>();

        foreach (PlayerTile tile in allTiles)
        {
            tile.SpawnCharacterWithHealth();
        }
    }
}
