using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.UI.PrefabElements.GameOverPanel;

namespace Assets.Scripts.UI
{
    public class GameOverPanelUIView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI winnerNameText;
        [SerializeField]
        private Transform playersListParent;

        private GameOverPanelUIController gameOverPanelUIController;
        public void SetController(GameOverPanelUIController controller) => gameOverPanelUIController = controller;
        public void SetVisibility(bool value) => gameObject.SetActive(value);
        public void SetWinnerNameText(string value) => winnerNameText.SetText(value);
        public void SetParentOfPlayerRankUIElement(TankPlayerRankUIView tankPlayerRankUIView)
        {
            tankPlayerRankUIView.transform.parent = playersListParent;
            tankPlayerRankUIView.transform.localScale = Vector3.one;
        }
    }
}
