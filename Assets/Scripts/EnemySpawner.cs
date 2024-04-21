using Assets.Scripts.Views.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private PlayerController enemy;
        [SerializeField]
        private SpawnPoint spawnPointPrefab;
        [SerializeField]
        private List<Transform> spawnPositions;
        //[SerializeField]
        //private int numberOfEnemies;

        public List<SpawnPoint> EnemySpawnPoints { get; set; }


        private void Start()
        {
            EnemySpawnPoints = new List<SpawnPoint>();
            SpawnEnemies();
        }

        public void SpawnEnemies()
        {
            for(int i=0; i< spawnPositions.Count; i++)
            {
                Quaternion spawnRotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0, 180), 0f);
                SpawnPoint enemySpawnPoint = Instantiate<SpawnPoint>(spawnPointPrefab,
                                                          spawnPositions[i].position,
                                                          spawnRotation);
                EnemySpawnPoints.Add(enemySpawnPoint);
            }
        }

        public IEnumerator SpawnEnemyOnConnect(NetworkManager.EnemiesJSON enemiesJSON)
        {
            foreach (NetworkManager.UserJSON enemyJSON in enemiesJSON.Enemies)
            {
                if (enemyJSON.Health <= 0)
                {
                    continue;
                }
                Vector3 position = new Vector3(enemyJSON.Position[0], enemyJSON.Position[1], enemyJSON.Position[2]);
                Quaternion rotation = Quaternion.Euler(enemyJSON.Rotation[0], enemyJSON.Rotation[1], enemyJSON.Rotation[2]);
                PlayerController playerController = Instantiate<PlayerController>(enemy, position, rotation);
                playerController.transform.name = enemyJSON.Name;

                playerController.IsLocalPlayer = false;
                Health health = playerController.transform.GetComponent<Health>();
                health.CurrentHealth = enemyJSON.Health;
                //h.OnChangeHealth();

                health.IsEnemy = true;
                yield return null;
            }
        }

    }
}
