using Assets.Scripts.Views;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField]
        private PlayerController player;
        public List<SpawnPoint> PlayerSpawnPoints;

        //[SerializeField]
        //private TankView tankView;
        //private Dictionary<TankTypes, Tank> tanks;

        // Start is called before the first frame update
        void Start()
        {
            //PlayerSpawnPoints = new List<SpawnPoint>();
            //CreateTank();
        }

        public void CreateTank()
        {
            //TankModel tankModel = new TankModel(player);
            //TankController tankController = new TankController(player, tankView);
            GameObject.Instantiate<PlayerController>(player,Vector3.zero, Quaternion.identity);

        }

        //public void CreateTank(PlayerTankTypes type)
        //{
        //    switch (type)
        //    {
        //        case PlayerTankTypes.Shark:
        //            TankModel tankModel = new TankModel(30, 40, tankList[0].color, tankList[0].type);
        //            TankController tankController = new TankController(tankModel, tankView);
        //            break;
        //        case PlayerTankTypes.Tiger:
        //            tankModel = new TankModel(30, 40, tankList[1].color, tankList[1].type);
        //            tankController = new TankController(tankModel, tankView);
        //            break;
        //        case PlayerTankTypes.Monkey:
        //            tankModel = new TankModel(30, 40, tankList[2].color, tankList[2].type);
        //            tankController = new TankController(tankModel, tankView);
        //            break;
        //    }
        //}

    }

    //[Serializable]
    //public class Tank
    //{
    //    public float movementSpeed;
    //    public float rotationSpeed;
    //    public EnemyTankTypes type;
    //    public Material color;
    //}

}
