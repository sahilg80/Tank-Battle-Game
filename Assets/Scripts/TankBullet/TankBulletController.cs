using Assets.Scripts.Main;
using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TankBullet
{
    public class TankBulletController
    {
        private TankBulletView tankBulletView;
        private ParticleSystem fireParticleSystem;
        private Rigidbody bulletRigidBody;
        private TankBulletScriptableObject tankBulletSO;
        private string idOfPlayerFrom;

        public TankBulletController(TankBulletView tankBulletView, ParticleSystem fireParticleSystem, TankBulletScriptableObject tankBulletSO)
        {
            this.tankBulletView = UnityEngine.Object.Instantiate(tankBulletView);
            this.fireParticleSystem = UnityEngine.Object.Instantiate(fireParticleSystem);
            this.tankBulletSO = tankBulletSO;
            Initialize();
            OnEnable();
        }

        public void OnEnable()
        {
            idOfPlayerFrom = string.Empty;
            tankBulletView.gameObject.SetActive(true);
            fireParticleSystem.gameObject.SetActive(false);
        }

        private void Initialize()
        {
            tankBulletView.SetController(this);
            bulletRigidBody = tankBulletView.GetRigidBody();
        }

        public void SetPlayerFromId(string id) => idOfPlayerFrom = id;

        public void SetTransform(Vector3 position, Quaternion rotation)
        {
            tankBulletView.transform.position = position;
            tankBulletView.transform.rotation = rotation;
        }

        public void SetBulletVelocity(float launchForce, Vector3 direction)
        {
            bulletRigidBody.velocity = launchForce * direction;
        }

        public void OnCollisionWithObject(Collision collision)
        {
            CheckCollidedObject(collision.gameObject);
            GenerateParticleEffects(collision.GetContact(0).point);
            DeactivateBullet();
        }

        private void CheckCollidedObject(GameObject collidedObject)
        {
            TankPlayerHealth health = collidedObject.GetComponent<TankPlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(tankBulletSO.BulletDamage, idOfPlayerFrom);
            }
        }

        private void GenerateParticleEffects(Vector3 point)
        {
            if (fireParticleSystem != null)
            {
                //GameObject bulletParticleEffectObject = GameObject.Instantiate(fireParticleSystem.gameObject);
                fireParticleSystem.gameObject.SetActive(true);
                fireParticleSystem.transform.position = point;
                fireParticleSystem.Play();
                //fireParticleSystem.gameObject.SetActive(false);
                //GameObject.Destroy(bulletParticleEffectObject, 2f);
            }
        }

        private void DeactivateBullet()
        {
            GameService.Instance.TankBulletService.ReturnTankBullet(this);
            tankBulletView.gameObject.SetActive(false);
        }

    }
}
