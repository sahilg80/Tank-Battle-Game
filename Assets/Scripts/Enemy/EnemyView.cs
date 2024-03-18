using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Assets.Scripts.Enemy
{
    public class EnemyView : BaseView
    {
        private EnemyController enemyController;

        [SerializeField]
        private NavMeshAgent agent;
        private float rotationSpeed;
        [SerializeField]
        private MeshRenderer[] renderers;
        [SerializeField]
        private EnemyShooting enemyShooting;
        [SerializeField]
        private Image healthBarImage;
        private float chasingRate;
        private Coroutine movementCoroutine;
        private Coroutine rotationCoroutine;
        private float currentHealth;

        private void OnEnable()
        {
            EventService.Instance.OnGameEnd.AddListener(OnTargetTankKilled);
        }
        private void OnDisable()
        {
            EventService.Instance.OnGameEnd.RemoveListener(OnTargetTankKilled);
        }

        private void Start()
        {
            //chasingRate = 2f;
            //currentHealth = 10f;
        }

        private void Update()
        {
            
        }

        private void OnTargetTankKilled(bool value)
        {
            StopAllCoroutines(); 
            enemyShooting.StopFire();
            agent.isStopped = true;
            this.enabled = false;
        }

        public void InitializeProperties(EnemySO enemyData)
        {
            enemyShooting.SetFireRate(enemyData.FireRate);
            enemyShooting.SetDamageValue(enemyData.Damage);
            enemyShooting.SetLaunchForce(enemyData.LaunchForce);
            rotationSpeed = enemyData.RotationSpeed;
            healthBarImage.fillAmount = 1;
            chasingRate = 2f;
            currentHealth = 10f;
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
        public void SetTankColor(Material color)
        {
            foreach (var renderer in renderers)
            {
                renderer.material = color;
            }
        }

        public void ReachToTarget()
        {
            SetDestination(enemyController.GetTargetPosition());
            if(movementCoroutine !=null)
            {
                StopCoroutine(movementCoroutine);
                movementCoroutine = null;
            }
            movementCoroutine = StartCoroutine(ChaseTarget());
        }

        public void TargetRelocated()
        {
            if (rotationCoroutine!=null)
            {
                StopCoroutine(rotationCoroutine);
                rotationCoroutine = null;
            }
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
            if (movementCoroutine != null)
            {
                StopCoroutine(movementCoroutine);
                movementCoroutine = null;
            }
            if(rotationCoroutine != null)
            {
                StopCoroutine(rotationCoroutine);
                rotationCoroutine = null;
            }
            rotationCoroutine = StartCoroutine(RotateTowardsTarget());
            agent.isStopped = true;
            enemyShooting.EnemyFire();
        }

        public void SetController(EnemyController controller)
        {
            this.enemyController = controller;
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
        public override void OnAttacked(float damage)
        {
            if (currentHealth <= 0) return;
            
            currentHealth = currentHealth - damage;
            healthBarImage.fillAmount = currentHealth / 10;

            if (currentHealth <= 0)
            {
                GameService.Instance.UpdateEnemiesKilled();
                ObjectPoolManager.Instance.DeSpawnObject(this.gameObject);
            }
        }
    }
}
