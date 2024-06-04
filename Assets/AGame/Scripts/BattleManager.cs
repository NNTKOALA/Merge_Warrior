using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private Button fightButton;

    private bool isAttack;

    // Start is called before the first frame update
    void Start()
    {
        isAttack = false;

        if (fightButton != null)
        {
            fightButton.onClick.AddListener(StartBattle);
        }
        else
        {
            Debug.LogWarning("Fight button is not assigned in the inspector");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBattle()
    {
        if (inGameUI != null)
        {
            inGameUI.SetActive(false);
        }
        else
        {
            Debug.LogWarning("In-game UI is not assigned in the inspector");
        }

        StartFighting();
    }

    private void StartFighting()
    {
        Debug.Log("Battle has started!");
    }
}
