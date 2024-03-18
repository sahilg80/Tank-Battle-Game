
using UnityEngine;

namespace Assets.Scripts
{
    public class TankModel
    {
        private TankController tankController;
        public PlayerSO DataObject { get; private set; }

        public TankModel(PlayerSO playerObj)
        {
            this.DataObject = playerObj;
        }

        public void SetController(TankController tankController)
        {
            this.tankController = tankController;
        }

    }
}
