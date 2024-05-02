using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.UI.PrefabElements.GameOverPanel
{
    public class TankPlayerRankUIController
    {
        private TankPlayerRankUIView tankPlayerRankUIView;
        public TankPlayerRankUIView TankPlayerRankUIView => tankPlayerRankUIView;
        public TankPlayerRankUIController(TankPlayerRankUIView tankPlayerRankUIView)
        {
            this.tankPlayerRankUIView = UnityEngine.Object.Instantiate<TankPlayerRankUIView>(tankPlayerRankUIView);
        }
        public void SetPlayerNameText(string value) => tankPlayerRankUIView.SetPlayerNameText(value);
        public void SetKillsText(string value) => tankPlayerRankUIView.SetKillsText(value);
    }
}
