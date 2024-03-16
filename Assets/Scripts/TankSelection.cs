
using Assets.Scripts.Enemy;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class TankSelection : MonoBehaviour
    {
        [SerializeField]
        private TankSpawner tankSpawner;
        [SerializeField]
        private EnemySpawner enemySpawner;
        [SerializeField]
        private Canvas canvas;
        private TankController tankController;

        private void OnEnable()
        {
            EventService.Instance.OnTankSelection.AddListener(SpawnEnemies);
        }

        public void SelectTank(int num)
        {
            TankTypes type = (TankTypes)num;

            tankController = tankSpawner.CreateTank(type);
            canvas.enabled = false;
            //this.gameObject.SetActive(false);
            EventService.Instance.OnTankSelection.InvokeEvent();
        }

        private void SpawnEnemies()
        {
            StartCoroutine(Spawn());
        }

        IEnumerator Spawn()
        {
            yield return new WaitForSeconds(0.7f);
            enemySpawner.CreateEnemyTank(EnemyTank.Cat, tankController.TankView.transform);
        }

        private void OnDisable()
        {
            EventService.Instance.OnTankSelection.RemoveListener(SpawnEnemies);
        }

    }
}
