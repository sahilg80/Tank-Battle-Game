using Assets.Scripts.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyDetector : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            EnemyView enemyView = other.GetComponentInParent<EnemyView>();
            if (enemyView != null)
            {
                enemyView.TargetDetected();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            EnemyView enemyView = other.GetComponentInParent<EnemyView>();
            if (enemyView != null)
            {
                enemyView.TargetRelocated();
            }
        }

    } 
}
