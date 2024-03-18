
using Assets.Scripts.Enemy;
using Assets.Scripts.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class TankSelection : MonoBehaviour
    {
        [SerializeField]
        private GameObject tankSelectionUI;
        [SerializeField]
        private GameObject gamePlayPanelUI;
        [SerializeField]
        private TankSpawner tankSpawner;
        [SerializeField]
        private EnemySpawner enemySpawner;
        private TankController tankController;
        //[SerializeField]
        //private WaveConfigurationSO waveConfiguration;
        [SerializeField]
        private List<WaveConfigurationSO> waveConfigurations;

        //[SerializeField]
        //private List<Transform> spawnLocations;
        //[SerializeField]
        //private List<EnemyTank> enemyTankTypes;

        private void OnEnable()
        {
            EventService.Instance.OnNewWaveStart.AddListener(OnStartNewWave);
        }

        private void OnDisable()
        {
            EventService.Instance.OnNewWaveStart.RemoveListener(OnStartNewWave);
        }
        public void SelectTank(int num)
        {
            TankTypes type = (TankTypes)num;

            tankController = tankSpawner.CreateTank(type);
            //canvas.enabled = false;
            tankSelectionUI.SetActive(false);
            gamePlayPanelUI.SetActive(true);
            GameService.Instance.SetTotalWaves(waveConfigurations.Count);
            EventService.Instance.OnNewWaveStart.InvokeEvent(1);
        }

        private void OnStartNewWave(int value)
        {
            GameService.Instance.SetTotalEnemiesInCurrentWave(waveConfigurations[value-1].EnemyTankTypes.Count);
            StartCoroutine(Spawn(value-1));
        }

        //private void SpawnEnemies()
        //{
        //    StartCoroutine(Spawn());
        //}

        IEnumerator Spawn(int value)
        {
            yield return new WaitForSeconds(0.7f);
            for (int i = 0; i < waveConfigurations[value].SpawnLocations.Count; i++) 
            {
                enemySpawner.CreateEnemyTank(waveConfigurations[value].EnemyTankTypes[i], tankController, waveConfigurations[value].SpawnLocations[i]); 
            }
            //this.gameObject.SetActive(false);
        }

    }
}
