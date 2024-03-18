using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService
{
    private static GameService instance;
    public static GameService Instance 
    { 
        get
        {
            if(instance == null)
            {
                instance = new GameService();
            }
            return instance;
        } 
    }
    private int enemiesKilled;
    private int enemiesKilledInCurrentWave;
    private int currentWaveNum;
    private int totalEnemiesInCurrentWave;
    public int TotalWaves { get; private set; }

    private GameService()
    {
        enemiesKilled = 0;
        currentWaveNum = 1;
    }

    public void SetTotalWaves(int value)
    {
        enemiesKilled = 0;
        currentWaveNum = 1;
        TotalWaves = value;
    }

    public void SetTotalEnemiesInCurrentWave(int value)
    {
        totalEnemiesInCurrentWave = value;
        enemiesKilledInCurrentWave = 0;
    }

    public void UpdateEnemiesKilled()
    {
        enemiesKilled++;
        enemiesKilledInCurrentWave++;
        EventService.Instance.OnEnemyTankKilled.InvokeEvent(enemiesKilled);
        if (totalEnemiesInCurrentWave == enemiesKilledInCurrentWave)
        {
            if (currentWaveNum >= TotalWaves)
            {
                Debug.Log("enemies killed");
                EventService.Instance.OnGameEnd.InvokeEvent(true);
                return;
            }
            currentWaveNum++;
            EventService.Instance.OnNewWaveStart.InvokeEvent(currentWaveNum);
        }
    }

}
