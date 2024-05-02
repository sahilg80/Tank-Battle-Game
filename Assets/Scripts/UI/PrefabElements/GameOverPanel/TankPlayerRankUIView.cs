using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.UI.PrefabElements.GameOverPanel
{
    public class TankPlayerRankUIView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI playerNameText;
        [SerializeField]
        private TextMeshProUGUI playerKillsText;

        public void SetPlayerNameText(string value) => playerNameText.SetText(value);
        public void SetKillsText(string value) => playerKillsText.SetText(value);

    }
}
