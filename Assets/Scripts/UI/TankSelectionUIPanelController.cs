using Assets.Scripts.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.Scripts.Utilities.Enums;

namespace Assets.Scripts.UI
{
    public class TankSelectionUIPanelController
    {
        private TankSelectionUIPanelView tankSelectionUIPanelView;

        public TankSelectionUIPanelController(TankSelectionUIPanelView tankSelectionUIPanelView)
        {
            this.tankSelectionUIPanelView = tankSelectionUIPanelView;
            Initialize();
        }

        private void Initialize()
        {
            tankSelectionUIPanelView.SetController(this);
            SetVisibility(false);
        }

        public void SetVisibility(bool value) => tankSelectionUIPanelView.SetVisibility(value);

        public void SetSelectedTank(TankPlayerType type)
        {
            GameService.Instance.ServerConnectorService.SetSelectedTankPlayerType(type);
            SetVisibility(false);
            GameService.Instance.UIService.NetworkJoinUIPanelController.SetVisibility(true);
        }
    }
}
