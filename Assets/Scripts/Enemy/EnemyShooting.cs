using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyShooting : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody Bullet;
        [SerializeField]
        private Transform FireBarrelTransform;
        [SerializeField]
        private ParticleSystem ParticleSystem;
        [SerializeField]
        private float launchForce;
        private float fireRate;
        private Coroutine shootingCoroutine;

        public void SetLaunchForce(float value)
        {
            launchForce = value;
        }

        public void SetFireRate(float value)
        {
            fireRate = value;
        }

        public void EnemyFire()
        {
            shootingCoroutine = StartCoroutine(ShootTarget());
        }

        IEnumerator ShootTarget()
        {
            WaitForSeconds delay = new WaitForSeconds(fireRate);
            while (true)
            {
                Fire();
                yield return delay;
            }
        }

        private void Fire()
        {
            // Create an instance of the shell and store a reference to it's rigidbody.
            GameObject bulletObject = ObjectPoolManager.Instance.SpawnObject(Bullet.gameObject);
            bulletObject.transform.position = FireBarrelTransform.position;
            bulletObject.transform.rotation = FireBarrelTransform.rotation;

            //Rigidbody shellInstance = Instantiate(Bullet, FireBarrelTransform.position, FireBarrelTransform.rotation) as Rigidbody;
            Rigidbody shellInstance = bulletObject.GetComponent<Rigidbody>();

            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = launchForce * FireBarrelTransform.up; ;

            // Play smoke effect
            ParticleSystem.Play();
        }

        public void StopFire()
        {
            StopCoroutine(shootingCoroutine);
        }

    }
}
