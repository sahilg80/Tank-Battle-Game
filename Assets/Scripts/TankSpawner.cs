using System;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class TankSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<PlayerSO> tankTypesList;
        [SerializeField]
        private TankView tankView;
        //private Dictionary<TankTypes, Tank> tanks;

        // Start is called before the first frame update
        void Start()
        {
        }

        public TankController CreateTank(TankTypes type)
        {
            PlayerSO playerTankData = GetObjectByType(type);
            TankModel playerTankModel = new TankModel(playerTankData);
            TankController playerTankController = new TankController(playerTankModel, tankView);
            return playerTankController;
        }

        private PlayerSO GetObjectByType(TankTypes type)
        {
            foreach (PlayerSO tank in tankTypesList)
            {
                if (tank.Type == type) return tank;
            }
            return null;
        }
    }

}
