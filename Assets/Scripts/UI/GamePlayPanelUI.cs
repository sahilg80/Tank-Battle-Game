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
        [SerializeField]
        private TextMeshProUGUI wavesStatusText;
        [SerializeField]
        private GameObject gameOverPanelUI;
        [SerializeField]
        private GameObject gameWinText;
        [SerializeField]
        private GameObject gameLoseText;

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
            StartCoroutine(ShowWaveStatusTextInUI(value));
            //}
            //StartCoroutine(ShowWaveNumberInUI());
        }

        private void SetTanksKilled(int value)
        {
            totalEnemiesKilled.SetText(value.ToString());
        }

        private void SetWaveStatusText(string value)
        {
            wavesStatusText.SetText(value);
        }

        private void DisableGamePlayPanel(bool value)
        {
            StartCoroutine(GameEndUIActions(value));
            //this.gameObject.SetActive(false);
        }

        private void EnableGameOverPanel(bool value)
        {
            gameWinText.SetActive(value);
            gameLoseText.SetActive(!value);
            gameOverPanelUI.SetActive(true);
            this.gameObject.SetActive(false);
        }

        IEnumerator GameEndUIActions(bool value)
        {
            SetWaveStatusText("Completed All Waves");
            yield return new WaitForSeconds(2f);
            SetWaveStatusText(string.Empty);
            EnableGameOverPanel(value);
        }

        IEnumerator ShowWaveNumberInUI()
        {
            waveNumberTextHolder.SetActive(true);
            yield return new WaitForSeconds(2f);
            waveNumberTextHolder.SetActive(false);
        }

        IEnumerator ShowWaveStatusTextInUI(int value)
        {
            if (value > 1)
            {
                SetWaveStatusText("Completed Wave " + (value-1).ToString());
                yield return new WaitForSeconds(2f);
                SetWaveStatusText(string.Empty);
            }
            StartCoroutine(ShowWaveNumberInUI());
        }
    }
}
