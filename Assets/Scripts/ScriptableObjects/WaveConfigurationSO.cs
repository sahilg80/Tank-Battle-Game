using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WaveSO", menuName = "ScriptableObject/WaveConfiguration")]
    public class WaveConfigurationSO : ScriptableObject
    {
        public List<Vector3> SpawnLocations;
        public List<EnemySO> EnemyTankTypes;
    }
}
