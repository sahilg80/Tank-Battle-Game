using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class TankSelectionUIPanelView : MonoBehaviour
    {
        [SerializeField]
        private Button redTankPlayer;
        [SerializeField]
        private Button blueTankPlayer;
        [SerializeField]
        private Button greenTankPlayer;
        private TankSelectionUIPanelController tankSelectionUIPanelController;

        private void OnEnable()
        {
            redTankPlayer.onClick.AddListener(OnSelectRedTankPlayer);
            blueTankPlayer.onClick.AddListener(OnSelectBlueTankPlayer);
            greenTankPlayer.onClick.AddListener(OnSelectGreenTankPlayer);
        }

        private void OnDisable()
        {
            redTankPlayer.onClick.RemoveListener(OnSelectRedTankPlayer);
            blueTankPlayer.onClick.RemoveListener(OnSelectBlueTankPlayer);
            greenTankPlayer.onClick.RemoveListener(OnSelectGreenTankPlayer);
        }

        public void SetVisibility(bool value) => gameObject.SetActive(value);

        public void SetController(TankSelectionUIPanelController controller) => tankSelectionUIPanelController = controller;

        private void OnSelectRedTankPlayer() => tankSelectionUIPanelController.SetSelectedTank(Utilities.Enums.TankPlayerType.Red);
        private void OnSelectBlueTankPlayer() => tankSelectionUIPanelController.SetSelectedTank(Utilities.Enums.TankPlayerType.Blue);
        private void OnSelectGreenTankPlayer() => tankSelectionUIPanelController.SetSelectedTank(Utilities.Enums.TankPlayerType.Green);
    }
}
