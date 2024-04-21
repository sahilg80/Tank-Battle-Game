using System;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Views.Views
{
    public class EnemyView : BaseView
    {
        [SerializeField]
        private Health health;
        [SerializeField]
        private Rigidbody rb;
        public bool IsLocalPlayer;
        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        public override void RecieveDamage(float value)
        {
            //health.UpdateHealth(value);
        }
    }
}
