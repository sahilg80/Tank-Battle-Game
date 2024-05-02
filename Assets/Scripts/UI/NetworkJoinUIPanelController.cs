using Assets.Scripts.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.UI
{
    public class NetworkJoinUIPanelController
    {
        private NetworkJoinUIPanelView networkJoinUIPanelView;

        public NetworkJoinUIPanelController(NetworkJoinUIPanelView startUIPanelView)
        {
            this.networkJoinUIPanelView = startUIPanelView;
            Initialize();
        }

        private void Initialize()
        {
            networkJoinUIPanelView.SetController(this);
            SetVisibility(false);
        }

        public void SetVisibility(bool value) => networkJoinUIPanelView.SetVisibility(value);

        public void OnClickJoinButton()
        {
            SetVisibility(false);
            GameService.Instance.UIService.LoadingPanelUIController.SetVisibility(true);
            GameService.Instance.EventService.OnClickGameJoin.InvokeEvent();
        }

        public string GetInputPlayerName() => networkJoinUIPanelView.GetInputPlayerName();
    }
}
