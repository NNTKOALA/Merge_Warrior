using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class EnemyPrefabData
    {
        public CharLevel enemyLevel;
        public CharType enemyType;
        public GameObject enemyPrefab;
        public Vector3 position;
    }

    [SerializeField][Range(0, 50)] int poolSize = 5;

    GameObject[] pool;

    public List<EnemyPrefabData> enemyPrefabs = new List<EnemyPrefabData>();

    void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            var prefabData = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            pool[i] = Instantiate(prefabData.enemyPrefab, transform);
            pool[i].transform.position = prefabData.position;
            pool[i].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                var prefabData = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
                pool[i].transform.position = prefabData.position;
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();            
        }
    }
}
