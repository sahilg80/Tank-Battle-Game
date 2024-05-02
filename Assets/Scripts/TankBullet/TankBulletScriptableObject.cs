using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TankBullet
{
    [CreateAssetMenu(fileName = "TankBulletSO", menuName = "ScriptableObject/TankBulletSO")]
    public class TankBulletScriptableObject : ScriptableObject
    {
        public float BulletDamage;
    }
}
