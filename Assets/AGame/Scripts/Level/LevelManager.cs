using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static PlayerTile;

public class LevelManager : MonoBehaviour
{
    public LevelData levelData;
    private int currentLevelIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(currentLevelIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levelData.levels.Length)
        {
            Level level = levelData.levels[levelIndex];

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
            }

            foreach (EnemyTile tile in level.enemyTiles)
            {
                Vector3 position = new Vector3(tile.x, 0, tile.z);
                GameObject prefab = CharacterData.Instance.GetCharacterPrefab(tile.characterType, tile.characterLevel);

                if (prefab == null)
                {
                    Debug.LogError("Prefab is null. Check GetCharacterPrefab method.");
                    return;
                }

                GameObject enemy = Instantiate(prefab, position, Quaternion.Euler(0, 180, 0));
                enemy.tag = "Enemy";
            }

            GameManager.Instance.currentLevelIndex = levelIndex + 1;
            GameManager.Instance.UpdateLevelText();
            GameManager.Instance.ResetBattle();
        }
    }

    public void NextLevel()
    {
        if (currentLevelIndex + 1 <= levelData.levels.Length)
        {
            currentLevelIndex++;
            LoadLevel(currentLevelIndex);
        }
        else
        {
            Debug.Log("You win the game!!!!!!");
        }
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevelIndex);
    }
}