using System;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class TankSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<Tank> tankList;
        [SerializeField]
        private TankView tankView;
        //private Dictionary<TankTypes, Tank> tanks;

        // Start is called before the first frame update
        void Start()
        {
        }

        public TankController CreateTank(TankTypes type)
        {
            TankModel tankModel = null;

            TankController tankController = null;

            switch (type)
            {
                case TankTypes.Green:
                    tankModel = new TankModel(30, 40, tankList[0].color, tankList[0].type);
                    tankController = new TankController(tankModel, tankView);
                    break;
                case TankTypes.Red:
                    tankModel = new TankModel(30, 40, tankList[1].color, tankList[1].type);
                    tankController = new TankController(tankModel, tankView);
                    break;
                case TankTypes.Blue:
                    tankModel = new TankModel(30, 40, tankList[2].color, tankList[2].type);
                    tankController = new TankController(tankModel, tankView);
                    break;
            }
            return tankController;
        }

    }

    [Serializable]
    public class Tank
    {
        public float movementSpeed;
        public float rotationSpeed;
        public TankTypes type;
        public Material color;
    }

}
