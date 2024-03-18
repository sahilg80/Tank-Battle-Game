using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class TankView : BaseView
    {
        private TankController tankController;

        [SerializeField]
        private Rigidbody rb;
        private float movementDir;
        private float rotationDir;
        [SerializeField]
        private MeshRenderer[] renderers;
        private float currentHealth;
        [SerializeField]
        private TankShooting tankShooting;
        [SerializeField]
        private Image healthBarImage;
        private GameObject cameraObject;

        private void Start()
        {
            cameraObject = GameObject.FindGameObjectWithTag("CameraHolder");
            cameraObject.transform.position = new Vector3(-9.578f, 17.352f, -9.242f);
            cameraObject.transform.parent = this.gameObject.transform;
            currentHealth = 10f;
        }

        private void Update()
        {
            Movement();
            if(movementDir != 0 && tankController!=null)
            {
                tankController.Move(movementDir, tankController.GetMovementSpeed());
            }
            Rotation();
            if(rotationDir != 0 && tankController != null)
            {
                tankController.Rotate(rotationDir, tankController.GetRotationSpeed());
            }
        }

        public void SetController(TankController tankController)
        {
            this.tankController = tankController;
        }

        public Rigidbody GetRigidBody()
        {
            return rb;
        }

        public void InitializeProperties(PlayerSO playerData)
        {
            tankShooting.SetLaunchForce(playerData.LaunchForce);
            tankShooting.SetDamageValue(playerData.Damage);
        }

        private void Movement()
        {
            movementDir = Input.GetAxis("Vertical");
        }

        private void Rotation()
        {
            rotationDir = Input.GetAxis("Horizontal");
        }

        public void SetTankColor(Material color)
        {
            foreach(var renderer in renderers)
            {
                renderer.material = color;
            }
        }
        public override void OnAttacked(float damage)
        {
            if (currentHealth <= 0) return;
            
            currentHealth = currentHealth - damage;
            healthBarImage.fillAmount = currentHealth / 10;

            if (currentHealth <= 0)
            {
                cameraObject.transform.parent = null;
                EventService.Instance.OnGameEnd.InvokeEvent(false);
                ObjectPoolManager.Instance.DeSpawnObject(this.gameObject);
            }
        }
    }
}
