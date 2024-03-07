using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class TankController
    {
        private TankModel tankModel;
        private TankView tankView;
        private Rigidbody rb;

        public TankController(TankModel tankModel, TankView tankView)
        {
            this.tankModel = tankModel;
            
            this.tankView = GameObject.Instantiate<TankView>(tankView);

            tankModel.SetController(this);
            this.tankView.SetController(this);
            rb = this.tankView.GetRigidBody();
        }

        public void Move(float dir, float speed)
        {
            rb.velocity = tankView.transform.forward * dir * speed;
        }

        public void Rotate(float dir, float speed)
        {
            Vector3 rot = new Vector3(0, dir * speed * Time.deltaTime, 0);
            Quaternion deltaRot = Quaternion.Euler(rot);
            rb.rotation = tankView.transform.rotation * deltaRot;
        }

        public float GetMovementSpeed()
        {
            return tankModel.movementSpeed;
        }
        public float GetRotationSpeed()
        {
            return tankModel.rotationSpeed;
        }

    }
}
