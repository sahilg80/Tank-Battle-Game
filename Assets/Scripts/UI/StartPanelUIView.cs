using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class StartPanelUIView : MonoBehaviour
    {
        [SerializeField]
        private Button playButton;
        [SerializeField]
        private Button quitButton;

        private StartPanelUIController startPanelUIController;

        private void OnEnable()
        {
            playButton.onClick.AddListener(OnClickPlayButton);
            quitButton.onClick.AddListener(OnClickQuitButton);
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveListener(OnClickPlayButton);
            quitButton.onClick.RemoveListener(OnClickQuitButton);
        }

        public void SetController(StartPanelUIController controller) => startPanelUIController = controller;

        public void SetVisibility(bool value)
        {
            this.gameObject.SetActive(value);
        }

        private void OnClickPlayButton() => startPanelUIController.OnClickPlayButton();

        private void OnClickQuitButton() => startPanelUIController.OnClickQuitButton();

    }
}
