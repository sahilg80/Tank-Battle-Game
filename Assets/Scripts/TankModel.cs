
using UnityEngine;

namespace Assets.Scripts
{
    public class TankModel
    {
        private TankController tankController;
        public float movementSpeed { get; private set; }
        public float rotationSpeed { get; private set; }
        public TankTypes type { get; private set; }
        public Material color { get; private set; }

        public TankModel(float movementSpeed, float rotationSpeed, Material color, TankTypes type)
        {
            this.movementSpeed = movementSpeed;
            this.rotationSpeed = rotationSpeed;
            this.color = color;
            this.type = type;
        }

        public void SetController(TankController tankController)
        {
            this.tankController = tankController;
        }

    }
}
