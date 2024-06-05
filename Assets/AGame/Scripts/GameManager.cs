using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float playerMoney = 200;
    public bool isPlaying;

    private void Awake()
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
        PauseGame();
        UIManager.Instance.UpdateMoney();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinGame()
    {
        UIManager.Instance.SwitchToWinUI();
        PauseGame();
    }

    public void LoseGame() 
    {
        UIManager.Instance.SwitchToLoseUI();
        PauseGame();
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
        }
    }

    public void AddMoney(float amount)
    {
        playerMoney += amount;
        UIManager.Instance.UpdateMoney();
    }

    public string FormatMoney(float amount)
    {
        if (amount >= 1000000)
        {
            return (amount / 1000000f).ToString("F0") + "M";
        }
        else if (amount >= 1000)
        {
            return (amount / 1000f).ToString("F0") + "K";
        }
        else
        {
            return amount.ToString("F0");
        }
    }
}
