using UnityEngine;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private List<EnemySO> enemyList;
        [SerializeField]
        private EnemyView enemyView;
        [SerializeField]
        private Vector3 spawnPosition;

        private void Start()
        {

        }

        public void CreateEnemyTank(EnemyTank type, Transform target)
        {
            EnemySO enemy = GetObjectByType(type);
            EnemyModel enemyModel = new EnemyModel(enemy, target);
            EnemyController enemyController = new EnemyController(enemyModel, enemyView, spawnPosition );
        }

        private EnemySO GetObjectByType(EnemyTank type)
        {
            foreach (EnemySO enemy in enemyList)
            {
                if (enemy.Type == type) return enemy;
            }
            return null;
        }

    }
}
