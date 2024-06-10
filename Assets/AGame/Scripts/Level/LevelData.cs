using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public Level[] levels;
}

[System.Serializable]
public class Level
{
    public EnemyTile[] enemyTiles;
}

[System.Serializable]
public class EnemyTile
{
    public float x;
    public float z;
    public CharType characterType;
    public CharLevel characterLevel;
}