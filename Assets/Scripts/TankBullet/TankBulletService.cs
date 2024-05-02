using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TankBullet
{
    public class TankBulletService
    {
        //private TankBulletView tankBulletView;
        //private ParticleSystem fireParticleSystem;
        //private TankBulletScriptableObject tankBulletSO;
        private TankBulletPool tankBulletPool;

        public TankBulletService(TankBulletView tankBulletView, ParticleSystem fireParticleSystem, TankBulletScriptableObject tankBulletSO)
        {
            //this.tankBulletView = tankBulletView;
            //this.fireParticleSystem = fireParticleSystem;
            //this.tankBulletSO = tankBulletSO;
            tankBulletPool = new TankBulletPool(tankBulletView, fireParticleSystem, tankBulletSO);
        }

        public TankBulletController GetTankBullet() => tankBulletPool.GetBullet();

        public void ReturnTankBullet(TankBulletController controller) => tankBulletPool.ReturnBulletToPool(controller);
    }
}
