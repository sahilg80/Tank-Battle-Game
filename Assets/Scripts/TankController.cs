using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class TankController
    {
        private TankModel tankModel;
        public TankView TankView { get; private set; }
        private Rigidbody rb;

        public TankController(TankModel tankModel, TankView tankView)
        {
            this.tankModel = tankModel;
            
            this.TankView = ObjectPoolManager.Instance.SpawnObject<TankView>(tankView);// GameObject.Instantiate<TankView>(tankView);

            this.tankModel.SetController(this);
            this.TankView.SetController(this);
            rb = this.TankView.GetRigidBody();
            this.TankView.InitializeProperties(this.tankModel.DataObject);
            this.TankView.SetTankColor(this.tankModel.DataObject.Color);
        }

        public void Move(float dir, float speed)
        {
            rb.velocity = TankView.transform.forward * dir * speed;
        }

        public void Rotate(float dir, float speed)
        {
            Vector3 rot = new Vector3(0, dir * speed * Time.deltaTime, 0);
            Quaternion deltaRot = Quaternion.Euler(rot);
            rb.rotation = TankView.transform.rotation * deltaRot;
        }

        public float GetMovementSpeed()
        {
            return tankModel.DataObject.MovementSpeed;
        }
        public float GetRotationSpeed()
        {
            return tankModel.DataObject.RotationSpeed;
        }

    }
}
