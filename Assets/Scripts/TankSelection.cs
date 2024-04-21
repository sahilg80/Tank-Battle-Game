
using UnityEngine;

namespace Assets.Scripts
{
    public class TankSelection : MonoBehaviour
    {
        [SerializeField]
        private PlayerSpawner tankSpawner;

        public void SelectTank(PlayerSO player)
        {
            this.gameObject.SetActive(false);
        }

        //public void SelectTank(int num)
        //{
        //    PlayerTankTypes type = (PlayerTankTypes)num;

        //    tankSpawner.CreateTank(type);
        //    this.gameObject.SetActive(false);
        //}

    }
}
