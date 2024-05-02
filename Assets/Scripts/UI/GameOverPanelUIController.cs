using Assets.Scripts.Main;
using Assets.Scripts.UI.PrefabElements.GameOverPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.Scripts.SocketIONetwork.EventHandlerClasses;

namespace Assets.Scripts.UI
{
    public class GameOverPanelUIController
    {
        private GameOverPanelUIView gameOverPanelUIView;
        private TankPlayerRankUIPool tankPlayerRankUIPool;
        public GameOverPanelUIController(GameOverPanelUIView gameOverPanelUIView, TankPlayerRankUIView tankPlayerRankUIView)
        {
            this.gameOverPanelUIView = gameOverPanelUIView;
            this.tankPlayerRankUIPool = new TankPlayerRankUIPool(tankPlayerRankUIView);
            Initialize();
        }

        private void Initialize()
        {
            gameOverPanelUIView.SetController(this);
            SetVisibility(false);
        }

        public void GameOverDataShow(GameOverJSON gameOverJSON)
        {
            foreach(var playerRankData in gameOverJSON.PlayersList)
            {
                TankPlayerRankUIController tankPlayerRankUIController = tankPlayerRankUIPool.GetTankPlayerRankUI();
                tankPlayerRankUIController.SetKillsText(playerRankData.Kills.ToString());
                tankPlayerRankUIController.SetPlayerNameText(playerRankData.Name);
                gameOverPanelUIView.SetParentOfPlayerRankUIElement(tankPlayerRankUIController.TankPlayerRankUIView);
            }
            SetWinnerNameInUI(gameOverJSON.WinnerName);
            GameService.Instance.UIService.GamePlayPanelUIController.SetVisibility(false);
            SetVisibility(true);
        }

        private void SetVisibility(bool value) => gameOverPanelUIView.SetVisibility(value);
        private void SetWinnerNameInUI(string value) => gameOverPanelUIView.SetWinnerNameText("Winner " + value);
    }
}
