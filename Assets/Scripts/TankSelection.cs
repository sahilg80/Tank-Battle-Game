
using UnityEngine;

namespace Assets.Scripts
{
    public class TankSelection : MonoBehaviour
    {
        [SerializeField]
        private TankSpawner tankSpawner;
        
        public void SelectTank(int num)
        {
            TankTypes type = (TankTypes)num;

            tankSpawner.CreateTank(type);
            this.gameObject.SetActive(false);
        }

    }
}
