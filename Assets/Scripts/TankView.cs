using UnityEngine;

namespace Assets.Scripts
{
    public class TankView : MonoBehaviour
    {
        private TankController tankController;

        private void Start()
        {
            
        }

        public void SetController(TankController tankController)
        {
            this.tankController = tankController;
        }
    }
}
