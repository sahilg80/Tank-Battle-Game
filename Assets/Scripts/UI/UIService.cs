using Assets.Scripts.UI.PrefabElements.GameOverPanel;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIService : MonoBehaviour
    {
        [Header("Network Join Panel UI")]
        [SerializeField]
        private NetworkJoinUIPanelView networkJoinUIPanelView;
        private NetworkJoinUIPanelController networkJoinUIPanelController;
        public NetworkJoinUIPanelController NetworkJoinUIPanelController => networkJoinUIPanelController; 

        [Header("Start Panel")]
        [SerializeField]
        private StartPanelUIView startUIPanelView;
        private StartPanelUIController startPanelUIController;

        [Header("Game Play Panel")]
        [SerializeField]
        private GamePlayPanelUIView gamePlayUIPanelView;
        private GamePlayPanelUIController gamePlayPanelUIController;
        public GamePlayPanelUIController GamePlayPanelUIController => gamePlayPanelUIController;


        [Header("Loading Panel")]
        [SerializeField]
        private LoadingPanelUIView loadingPanelUIView;
        private LoadingPanelUIController loadingPanelUIController;
        public LoadingPanelUIController LoadingPanelUIController => loadingPanelUIController;

        [Header("Tank Selection Panel")]
        [SerializeField]
        private TankSelectionUIPanelView tankSelectionUIPanelView;
        private TankSelectionUIPanelController tankSelectionUIPanelController;
        public TankSelectionUIPanelController TankSelectionUIPanelController => tankSelectionUIPanelController;

        [Header("Game Over Panel")]
        [SerializeField]
        private GameOverPanelUIView gameOverPanelUIView;
        [SerializeField]
        private TankPlayerRankUIView tankPlayerRankUIView;
        private GameOverPanelUIController gameOverPanelUIController;
        public GameOverPanelUIController GameOverPanelUIController => gameOverPanelUIController;

        private void Start()
        {
            this.networkJoinUIPanelController = new NetworkJoinUIPanelController(networkJoinUIPanelView);
            this.startPanelUIController = new StartPanelUIController(startUIPanelView);
            this.gamePlayPanelUIController = new GamePlayPanelUIController(gamePlayUIPanelView);
            this.loadingPanelUIController = new LoadingPanelUIController(loadingPanelUIView);
            this.tankSelectionUIPanelController = new TankSelectionUIPanelController(tankSelectionUIPanelView);
            this.gameOverPanelUIController = new GameOverPanelUIController(gameOverPanelUIView, tankPlayerRankUIView);
        }

        public NetworkJoinUIPanelController GetStartUIPanelController() => networkJoinUIPanelController;
    }
}
