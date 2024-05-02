using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class LoadingPanelUIView : MonoBehaviour
    {
        private LoadingPanelUIController loadingPanelUIController;

        public void SetController(LoadingPanelUIController controller) => loadingPanelUIController = controller;
        public void SetVisibility(bool value)
        {
            this.gameObject.SetActive(value);
        }
    }
}
