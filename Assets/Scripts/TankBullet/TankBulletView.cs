using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TankBullet
{
    public class TankBulletView : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody rigidBodyBullet;
        private TankBulletController tankBulletController;
        //private float bulletDamage;
        //private GameObject shootPlayerFrom;
        //public GameObject PlayerFrom { get; set; }

        //public void SetShootPlayerFrom(GameObject player) => shootPlayerFrom = player;
        //public void SetBulletDamage(float value) => bulletDamage = value;
        public void SetController(TankBulletController controller) => tankBulletController = controller;

        public Rigidbody GetRigidBody() => rigidBodyBullet;

        private void OnCollisionEnter(Collision collision)
        {
            //GameObject hit = collision.gameObject;
            tankBulletController.OnCollisionWithObject(collision);

            //tankBulletController.CheckCollidedObject(collision.gameObject);

            //Destroy(this.gameObject);
            //OnCollideWithObject(collision.GetContact(0).point);
        }

    }
}
