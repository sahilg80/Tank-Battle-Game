using UnityEngine;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        //[SerializeField]
        //private List<EnemySO> enemyList;
        [SerializeField]
        private EnemyView enemyView;
        [SerializeField]
        private Vector3 spawnPosition;

        private void Start()
        {

        }

        public void CreateEnemyTank(EnemySO enemyData, TankController tankController, Vector3 position)
        {
            EnemyModel enemyModel = new EnemyModel(enemyData, tankController.TankView.transform);
            EnemyController enemyController = new EnemyController(enemyModel, enemyView, position);
        }

    }
}
