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
        public TankModel()
        {

        }

        public void SetController(TankController tankController)
        {
            this.tankController = tankController;
        }
    }
}
