﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemy
{
    public class EnemyController
    {
        private EnemyModel enemyModel;
        private EnemyView enemyView;
        private Rigidbody rb;

        public EnemyController(EnemyModel enemyModel, EnemyView enemyView, Vector3 position)
        {
            this.enemyModel = enemyModel;
            this.enemyView = GameObject.Instantiate<EnemyView>(enemyView);
            this.enemyView.transform.position = position;

            this.enemyModel.SetController(this);
            this.enemyView.SetController(this);
            this.enemyView.InitializeProperties(this.enemyModel.DataObject);
            //rb = this.tankView.GetRigidBody();
            this.enemyView.SetTankColor(this.enemyModel.DataObject.Color);
            MoveEnemy();
        } 

        private void MoveEnemy()
        {
            enemyView.SetSpeed(enemyModel.DataObject.MovementSpeed);
            enemyView.ReachToTarget();
        }

        public Vector3 GetTargetPosition()
        {
            return enemyModel.Target.position;
        }

    }
}
