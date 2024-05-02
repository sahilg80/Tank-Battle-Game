using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Utilities.Enums;

namespace Assets.Scripts.Player
{

    [CreateAssetMenu(fileName = "TankPlayerSO", menuName = "ScriptableObject/TankPlayer")]
    public class TankPlayerScriptableObject : ScriptableObject
    {
        public TankPlayerType TankType;
        public TankPlayerView TankPlayerView;
        public Material MeshColorMaterial;
        public float MovementSpeed;
        public float RotationSpeed;
        public float LaunchForce;
    }
}
