using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float playerMoney = 200;
    public bool isPlaying;

    public TextMeshProUGUI levelText;
    public int currentLevelIndex = 1;

    [SerializeField] private GameObject characterCardPrefab;
    private GameObject characterCardInstance;

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
    }

    // Start is called before the first frame update
    void Start()
    {
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
    }

    public void WinGame()
    {
        AudioManager.Instance.PlaySFX("Win");
        PauseGame();
        UIManager.Instance.SwitchToWinUI();
    }

    public void LoseGame() 
    {
        AudioManager.Instance.PlaySFX("Lose");
        PauseGame();
        UIManager.Instance.SwitchToLoseUI();
    }

    public void PauseGame()
    {
        isPlaying = false;
    }

    public void ResumeGame()
    {
        isPlaying = true;
    }

    public void CheckEnemiesStatus()
    {
        bool allEnemiesDown = true;
        Character[] enemies = FindObjectsOfType<Character>();
        foreach (Character enemy in enemies)
        {
            if (enemy != null)
            {
                allEnemiesDown = false;
                break;
            }
        }
        if (allEnemiesDown)
        {
            WinGame();
        }
    }

    public void CheckPlayerStatus()
    {
        bool allPlayersDown = true;
        Character[] players = FindObjectsOfType<Character>();
        foreach (Character player in players)
        {
            if (player != null)
            {
                allPlayersDown = false;
                break;
            }
        }
        if (allPlayersDown)
        {
            LoseGame();
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
}
