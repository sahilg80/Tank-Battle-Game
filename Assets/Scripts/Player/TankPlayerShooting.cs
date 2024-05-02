using Assets.Scripts.TankBullet;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class TankPlayerShooting : MonoBehaviour
    {

        [SerializeField]
        private TankBulletView bulletPrefabView;
        [SerializeField]
        private Transform fireBarrelTransform;
        [SerializeField]
        private ParticleSystem shootingParticleSystem;
        private float currentLaunchForce;

        public void SetLaunchForce(float value) => currentLaunchForce = value;

        public void Fire(TankBulletController bulletController)
        {
            bulletController.SetTransform(fireBarrelTransform.position, fireBarrelTransform.rotation);
            bulletController.SetBulletVelocity(currentLaunchForce, fireBarrelTransform.up);
            // Create an instance of the shell and store a reference to it's rigidbody.
            //GameObject bulletObject = GameObject.Instantiate(bulletPrefabView.gameObject); //ObjectPoolManager.Instance.SpawnObject(Bullet.gameObject);
            //bulletObject.transform.position = fireBarrelTransform.position;
            //bulletObject.transform.rotation = fireBarrelTransform.rotation;
            ////bulletObject.GetComponent<Bullet>().SetShootPlayerFrom(this.gameObject);
            
            //Rigidbody shellInstance = bulletObject.GetComponent<Rigidbody>();

            //// Set the shell's velocity to the launch force in the fire position's forward direction.
            //shellInstance.velocity = currentLaunchForce * fireBarrelTransform.up;

            // Play smoke effect
            shootingParticleSystem.Play();
        }

    }
}
