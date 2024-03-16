using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyController enemyController;

        [SerializeField]
        private NavMeshAgent agent;
        private float movementDir;
        private float rotationSpeed;
        [SerializeField]
        private MeshRenderer[] renderers;
        [SerializeField]
        private EnemyShooting enemyShooting;
        private float chasingRate;
        private Coroutine movementCoroutine;
        private Coroutine rotationCoroutine;

        private void Start()
        {
            chasingRate = 2f;
        }

        private void Update()
        {
            
        }

        private void SetDestination(Vector3 pos)
        {
            agent.SetDestination(pos);
        }

        public void SetSpeed(float speed)
        {
            agent.speed = speed;
            agent.stoppingDistance = 0.8f;
        }

        public void ReachToTarget()
        {
            SetDestination(enemyController.GetTargetPosition());
            movementCoroutine = StartCoroutine(ChaseTarget());
        }

        public void TargetRelocated()
        {
            StopCoroutine(rotationCoroutine);
            enemyShooting.StopFire();
            agent.isStopped = false;
            ReachToTarget();
        }

        IEnumerator ChaseTarget()
        {
            yield return null;
            WaitForSeconds delay = new WaitForSeconds(chasingRate);
            while (true)
            {
                yield return delay;
                SetDestination(enemyController.GetTargetPosition());
            }
        }

        IEnumerator RotateTowardsTarget()
        {
            while (true)
            {
                Vector3 direction = enemyController.GetTargetPosition() - transform.position;

                // Create a rotation to look at the target position
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Smoothly rotate towards the target rotation
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                yield return null;
            }
        }

        public void TargetDetected()
        {
            StopCoroutine(movementCoroutine);
            rotationCoroutine = StartCoroutine(RotateTowardsTarget());
            agent.isStopped = true;
            enemyShooting.EnemyFire();
        }

        public void SetController(EnemyController controller)
        {
            this.enemyController = controller;
        }

        public void InitializeProperties(EnemySO enemyData)
        {
            enemyShooting.SetFireRate(enemyData.FireRate);
            enemyShooting.SetLaunchForce(enemyData.LaunchForce);
            rotationSpeed = enemyData.RotationSpeed;
        }

        //public NavMeshAgent GetNavMeshAgent()
        //{
        //    return agent;
        //}

        //private void Movement()
        //{
        //    movementDir = Input.GetAxis("Vertical");
        //}

        //private void Rotation()
        //{
        //    rotationDir = Input.GetAxis("Horizontal");
        //}

        public void SetTankColor(Material color)
        {
            foreach (var renderer in renderers)
            {
                renderer.material = color;
            }
        }
    }
}
