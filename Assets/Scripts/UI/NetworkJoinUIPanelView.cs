using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class NetworkJoinUIPanelView : MonoBehaviour
    {
        [SerializeField]
        private InputField playerNameInput;
        [SerializeField]
        private Button joinGameButton;
        private NetworkJoinUIPanelController startUIPanelController;


        private void OnEnable()
        {
            joinGameButton.onClick.AddListener(OnClickJoinButton);
        }
        public void SetVisibility(bool value)
        {
            this.gameObject.SetActive(value);
        }
        public void SetController(NetworkJoinUIPanelController controller) => startUIPanelController = controller;

        private void OnDisable()
        {
            joinGameButton.onClick.RemoveListener(OnClickJoinButton);
        }

        public void OnClickJoinButton() => startUIPanelController.OnClickJoinButton();

        public string GetInputPlayerName() => playerNameInput.text;

    }
}
