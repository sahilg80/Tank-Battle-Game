using Assets.Scripts.Main;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class StartPanelUIController
    {
        private StartPanelUIView startPanelUIView;
        public StartPanelUIController(StartPanelUIView startPanelUIView)
        {
            this.startPanelUIView = startPanelUIView;
            Initialize();
        }

        private void Initialize()
        {
            startPanelUIView.SetController(this);
            startPanelUIView.SetVisibility(true);
        }

        public void OnClickPlayButton()
        {
            startPanelUIView.SetVisibility(false);
            GameService.Instance.UIService.TankSelectionUIPanelController.SetVisibility(true);
        }

        public void OnClickQuitButton() => Application.Quit();
        

    }
}
