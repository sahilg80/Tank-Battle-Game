using Assets.Scripts.Main;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.SocketIONetwork.EventHandlerClasses;

namespace Assets.Scripts.UI
{
    public class GamePlayPanelUIController
    {
        private GamePlayPanelUIView gamePlayPanelUIView;
        public GamePlayPanelUIController(GamePlayPanelUIView gamePlayPanelUIView)
        {
            this.gamePlayPanelUIView = gamePlayPanelUIView;
            Initialize();
        }
        ~GamePlayPanelUIController()
        {
            GameService.Instance.EventService.OnPlayerGameJoined.RemoveListener(OnPlayerConnectedToPlay);
        }

        private void Initialize()
        {
            GameService.Instance.EventService.OnPlayerGameJoined.AddListener(OnPlayerConnectedToPlay);
            gamePlayPanelUIView.SetController(this);
            SetVisibility(false);
        }

        public void SetVisibility(bool value) => gamePlayPanelUIView.SetVisibility(value);

        public void UpdatePlayerKillsText(string text) => gamePlayPanelUIView.SetPlayerKillsText(text);

        public void OnPlayerConnectedToPlay()
        {
            GameService.Instance.UIService.LoadingPanelUIController.SetVisibility(false);
            SetVisibility(true);
        }

        public void SetPlayerJoinedText(string text) => gamePlayPanelUIView.SetPlayerJoinedNameText(text);

        public void SetCountdownTimer(GameTimer gameTimerJSON)
        {
            gamePlayPanelUIView.SetCountdownTimer(FormatTime(gameTimerJSON.TimerText));
        }

        private string FormatTime(int timeInSeconds)
        {
            int minutes = Mathf.FloorToInt(timeInSeconds / 60);
            int seconds = Mathf.FloorToInt(timeInSeconds % 60);
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }
}
