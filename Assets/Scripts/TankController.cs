using Assets.Scripts.Views;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class TankController
    {
        //private TankModel tankModel;
        private TankView tankView;
        private Rigidbody rb;
        private PlayerSO playerData;

        public TankController(PlayerSO player, TankView tankView)
        {
            //this.tankModel = tankModel;
            playerData = player;
            this.tankView = GameObject.Instantiate<TankView>(tankView);

            //this.tankModel.SetController(this);
            this.tankView.SetController(this);
            Initialize();
        }

        public void Initialize()
        {
            rb = this.tankView.GetRigidBody();
            this.tankView.SetTankColor(playerData.Color);
            this.tankView.SetLaunchForce(playerData.LaunchForce);
        }

        public void Move(float dir)
        {
            rb.velocity = tankView.transform.forward * dir * playerData.MovementSpeed;
        }

        public void Rotate(float dir)
        {
            Vector3 rot = new Vector3(0, dir * playerData.RotationSpeed * Time.deltaTime, 0);
            Quaternion deltaRot = Quaternion.Euler(rot);
            rb.rotation = tankView.transform.rotation * deltaRot;
        }

        //public float GetMovementSpeed()
        //{
        //    return tankModel.movementSpeed;
        //}
        //public float GetRotationSpeed()
        //{
        //    return tankModel.rotationSpeed;
        //}

    }
}
