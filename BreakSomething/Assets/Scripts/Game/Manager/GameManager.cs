using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BattleManager battleManager;
    public SpawnManager spawnManager;
    public UIManager uiManager;
    public Observer observer;
    public CameraController cameraController;

    private void Awake()
    {
        battleManager.OnAsyncInit();
        spawnManager.OnAsyncInit();
        uiManager.OnAsyncInit();
        observer.OnAsyncInit();
        cameraController.OnAsyncInit();
    }

    private void Start()
    {
        battleManager.OnSyncInit();
        spawnManager.OnSyncInit();
        uiManager.OnSyncInit();
        observer.OnSyncInit();
        cameraController.OnSyncInit();
    }
}