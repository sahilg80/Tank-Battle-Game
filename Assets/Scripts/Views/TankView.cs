
using UnityEngine;

namespace Assets.Scripts.Views
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
        [SerializeField]
        private TankShooting tankShooting;
        [SerializeField]
        private Health health;
        public bool IsLocalPlayer;

        private void Start()
        {
            if (!IsLocalPlayer) return;
            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
            cam.transform.parent = this.gameObject.transform;
            cam.transform.position = new Vector3(0, 4, -10);
        }

        private void Update()
        {
            if (!IsLocalPlayer) return;
            Movement();
            tankController?.Move(movementDir);

            Rotation();
            tankController?.Rotate(rotationDir);
            Shoot();
        }

        public void SetController(TankController tankController)
        {
            this.tankController = tankController;
        }

        public void SetLaunchForce(float value)
        {
            tankShooting.SetLaunchForce(value);
        }

        public Rigidbody GetRigidBody()
        {
            return rb;
        }

        private void Movement()
        {
            movementDir = Input.GetAxis("Vertical");
        }

        private void Rotation()
        {
            rotationDir = Input.GetAxis("Horizontal");
        }

        private void Shoot()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                tankShooting.Fire();
            }
        }

        public void SetTankColor(Material color)
        {
            foreach(var renderer in renderers)
            {
                renderer.material = color;
            }
        }

        public override void RecieveDamage(float value)
        {
            //health.UpdateHealth(value);
        }
    }
}
