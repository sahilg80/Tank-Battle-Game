using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class TankController
    {
        private TankModel tankModel;
        private TankView tankView;

        public TankController(TankModel tankModel, TankView tankView)
        {
            this.tankModel = tankModel;
            this.tankView = tankView;

            GameObject.Instantiate(tankView.gameObject, tankView.gameObject.transform.position, Quaternion.identity);

            tankModel.SetController(this);
            tankView.SetController(this);
        }
    }
}
