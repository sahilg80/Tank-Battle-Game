using UnityEngine;

namespace Assets.Scripts
{
    public class TankView : MonoBehaviour
    {
        private TankController tankController;

        [SerializeField]
        private Rigidbody rb;
        private float movementDir;
        private float rotationDir;

        private void Start()
        {
            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
            cam.transform.parent = this.gameObject.transform;
            cam.transform.position = new Vector3(0, 4, -10);
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

        private void Movement()
        {
            movementDir = Input.GetAxis("Vertical");
        }

        private void Rotation()
        {
            rotationDir = Input.GetAxis("Horizontal");
        }
    }
}
