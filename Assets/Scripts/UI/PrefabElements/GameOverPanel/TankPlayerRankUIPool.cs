using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.UI.PrefabElements.GameOverPanel
{
    public class TankPlayerRankUIPool : GenericObjectPool<TankPlayerRankUIController>
    {
        private TankPlayerRankUIView tankPlayerRankUIView;
        public TankPlayerRankUIPool(TankPlayerRankUIView tankPlayerRankUIView)
        {
            this.tankPlayerRankUIView = tankPlayerRankUIView;
        }

        public TankPlayerRankUIController GetTankPlayerRankUI() => GetItem();

        public void ReturnTankPlayerRankUIToPool(TankPlayerRankUIController controller) => ReturnItem(controller);

        protected override TankPlayerRankUIController CreateItem() => new TankPlayerRankUIController(tankPlayerRankUIView);
    }
}
