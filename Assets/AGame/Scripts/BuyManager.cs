using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    public GameObject warriorPrefab;
    public GameObject archerPrefab;

    public Transform spawnPoint;

    //public BuyManager buyManager;

    // Start is called before the first frame update
    void Start()
    {
        //buyManager.SpawnWarriorUnit(0);
        //buyManager.SpawnArcherUnit(1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnWarriorUnit()
    {
        Debug.Log("Create Warrior Lv1");
        Instantiate(warriorPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void SpawnArcherUnit()
    {
        Debug.Log("Create Archer Lv1");
        Instantiate(archerPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
