using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GamePlayPanelUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI totalWavesPassed;
        [SerializeField]
        private TextMeshProUGUI currentWaveNum;
        [SerializeField]
        private TextMeshProUGUI totalEnemiesKilled;
        [SerializeField]
        private GameObject waveNumberTextHolder;

        private void Start()
        {

        }
        private void OnEnable()
        {
            EventService.Instance.OnEnemyTankKilled.AddListener(SetTanksKilled);
            EventService.Instance.OnNewWaveStart.AddListener(SetCurrentWaveNum);
            EventService.Instance.OnGameEnd.AddListener(DisableGamePlayPanel);
        }

        private void OnDisable()
        {
            EventService.Instance.OnEnemyTankKilled.RemoveListener(SetTanksKilled);
            EventService.Instance.OnNewWaveStart.RemoveListener(SetCurrentWaveNum);
            EventService.Instance.OnGameEnd.RemoveListener(DisableGamePlayPanel);
        }

        private void SetTotalWavesPassedNum(int value)
        {
            totalWavesPassed.SetText(value + "/" + GameService.Instance.TotalWaves);
        }

        private void SetCurrentWaveNum(int value)
        {
            currentWaveNum.SetText(value.ToString());
            SetTotalWavesPassedNum(value);
            StartCoroutine(ShowWaveNumberInUI());
        }

        private void SetTanksKilled(int value)
        {
            totalEnemiesKilled.SetText(value.ToString());
        }

        private void DisableGamePlayPanel(bool value)
        {
            this.gameObject.SetActive(false);
        }

        IEnumerator ShowWaveNumberInUI()
        {
            waveNumberTextHolder.SetActive(true);
            yield return new WaitForSeconds(2f);
            waveNumberTextHolder.SetActive(false);
        }
    }
}
