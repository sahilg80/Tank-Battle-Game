using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class UIService : MonoBehaviour
    {
        [SerializeField]
        private GameObject tankSelectionUI;
        [SerializeField]
        private GameObject startMenuUI;
        [SerializeField]
        private GameObject gameOverPanelUI;
        [SerializeField]
        private GameObject gameWinText;
        [SerializeField]
        private GameObject gameLoseText;

        private void OnEnable()
        {
            EventService.Instance.OnGameEnd.AddListener(EnableGameOverPanel);
        }
        private void OnDisable()
        {
            EventService.Instance.OnGameEnd.RemoveListener(EnableGameOverPanel);
        }
        private void Start()
        {
            gameOverPanelUI.SetActive(false);
            tankSelectionUI.SetActive(false);
            startMenuUI.SetActive(true);
        }

        public void OnClickPlayButton()
        {
            tankSelectionUI.SetActive(true);
            startMenuUI.SetActive(false);
        }

        public void OnClickQuitButton()
        {
            Application.Quit();
        }

        public void OnClickPlayAgainButton()
        {
            SceneManager.LoadScene(0);
        }

        private void EnableGameOverPanel(bool value)
        {
            gameWinText.SetActive(value);
            gameLoseText.SetActive(!value);
            gameOverPanelUI.SetActive(true);
        }

    }
}
