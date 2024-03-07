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

        // Start is called before the first frame update
        void Start()
        {
            CreateTank();
        }

        private void CreateTank()
        {
            TankModel tankModel = new TankModel(30, 40, tankList[0].color, tankList[0].type);
            TankController tankController = new TankController(tankModel, tankView);
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
