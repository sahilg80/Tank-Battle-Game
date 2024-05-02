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
            
            // Play smoke effect
            shootingParticleSystem.Play();
        }

    }
}
