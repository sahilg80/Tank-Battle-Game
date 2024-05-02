using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.UI
{
    public class LoadingPanelUIController
    {
        private LoadingPanelUIView loadingPanelUIView;
        public LoadingPanelUIController(LoadingPanelUIView loadingPanelUIView)
        {
            this.loadingPanelUIView = loadingPanelUIView; 
            Initialize();
        }

        private void Initialize()
        {
            loadingPanelUIView.SetController(this);
            SetVisibility(false);
        }
        public void SetVisibility(bool value) => loadingPanelUIView.SetVisibility(value);

    }
}
