using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyModel
    {
        private EnemyController enemyController;
        private float currentHealth;
        public EnemySO DataObject { get; private set; }
        public Transform Target { get; private set; }

        public EnemyModel(EnemySO enemyObj, Transform target)
        {
            this.DataObject = enemyObj;
            this.Target = target;
        }

        public void SetController(EnemyController controller)
        {
            this.enemyController = controller;
        }

    }
}
