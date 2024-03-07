using UnityEngine;


namespace Assets.Scripts
{
    public class TankSpawner : MonoBehaviour
    {
        [SerializeField]
        private TankView tankView;
        // Start is called before the first frame update
        void Start()
        {
            CreateTank();
        }

        private void CreateTank()
        {
            TankModel tankModel = new TankModel();
            TankController tankController = new TankController(tankModel, tankView);

        }

    }
}
