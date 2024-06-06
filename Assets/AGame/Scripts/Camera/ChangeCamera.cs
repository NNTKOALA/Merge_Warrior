using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField] private GameObject _cameraMain;
    [SerializeField] private GameObject _cameraInGame;
    // Start is called before the first frame update
    void Start()
    {
        _cameraMain.SetActive(true);
        _cameraInGame.SetActive(false);
    }

    public void ChangeCameraInGame()
    {
        _cameraMain.SetActive(false);
        _cameraInGame.SetActive(true);
    }

    public void ChangeCameraGameMenu()
    {
        _cameraMain.SetActive(true);
        _cameraInGame.SetActive(false);
    }
}
