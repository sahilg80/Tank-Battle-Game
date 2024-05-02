using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

namespace Assets.Scripts.UI
{
    public class GamePlayPanelUIView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI playerJoinedText;
        [SerializeField]
        private TextMeshProUGUI playerKillsText;
        [SerializeField]
        private TextMeshProUGUI countDownTimerText;

        private WaitForSeconds playerJoinedTextDelay;
        private Coroutine playerJoinCoroutine;
        private GamePlayPanelUIController gamePlayPanelUIController;

        private void Awake()
        {
            playerJoinedTextDelay = new WaitForSeconds(3f);
            playerJoinedText.SetText(string.Empty);
            SetPlayerKillsText(string.Empty);
        }

        public void SetController(GamePlayPanelUIController controller) => gamePlayPanelUIController = controller;

        public void SetVisibility(bool value) => this.gameObject.SetActive(value);

        public void SetPlayerKillsText(string text) => playerKillsText.SetText(text);

        public void SetPlayerJoinedNameText(string text)
        {
            if(playerJoinCoroutine != null)
            {
                StopCoroutine(playerJoinCoroutine);
            }
            playerJoinedText.SetText(text + " joined...");
            playerJoinCoroutine = StartCoroutine(ShowPlayerJoinedText());
        }

        private IEnumerator ShowPlayerJoinedText()
        {
            yield return playerJoinedTextDelay;
            playerJoinedText.SetText(string.Empty);
            playerJoinCoroutine = null;
        }

        public void SetCountdownTimer(string value) => countDownTimerText.SetText(value);

    }
}
