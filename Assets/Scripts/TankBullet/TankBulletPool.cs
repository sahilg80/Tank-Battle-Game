using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TankBullet
{
    public class TankBulletPool : GenericObjectPool<TankBulletController>
    {
        private TankBulletView bulletPrefabView;
        private ParticleSystem fireParticleSystem;
        private TankBulletScriptableObject tankBulletSO;

        public TankBulletPool(TankBulletView bulletView, ParticleSystem fireParticleSystem, TankBulletScriptableObject tankBulletSO)
        {
            bulletPrefabView = bulletView;
            this.fireParticleSystem = fireParticleSystem;
            this.tankBulletSO = tankBulletSO;
        }

        public TankBulletController GetBullet() => GetItem();

        public void ReturnBulletToPool(TankBulletController controller) => ReturnItem(controller);

        protected override TankBulletController CreateItem() => new TankBulletController(bulletPrefabView, fireParticleSystem, tankBulletSO);
    }
}
