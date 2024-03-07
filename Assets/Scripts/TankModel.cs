using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class TankModel
    {
        private TankController tankController;
        public float movementSpeed { get; private set; }
        public float rotationSpeed { get; private set; }
        public TankModel(float movementSpeed, float rotationSpeed)
        {
            this.movementSpeed = movementSpeed;
            this.rotationSpeed = rotationSpeed;
        }

        public void SetController(TankController tankController)
        {
            this.tankController = tankController;
        }
    }
}
