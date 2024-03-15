
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class TankShooting : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody Bullet;
        [SerializeField]
        private Transform FireBarrelTransform;     
        [SerializeField]
        private Slider AimSlider;
        [SerializeField]
        private ParticleSystem ParticleSystem;
        public float MinLaunchForce = 15f;        
        public float MaxLaunchForce = 30f;        
        public float MaxChargeTime = 0.75f;    

        private float currentLaunchForce;         
        private float chargeSpeed;                
        private bool isBulletFired;               

        private void OnEnable()
        {
            currentLaunchForce = MinLaunchForce;
            AimSlider.value = MinLaunchForce;
        }

        private void Start()
        {
            // The rate that the launch force charges up is the range of possible forces by the max charge time.
            chargeSpeed = (MaxLaunchForce - MinLaunchForce) / MaxChargeTime;
        }

        private void Update()
        {
            // The slider should have a default value of the minimum launch force.
            AimSlider.value = MinLaunchForce;

            // If the max force has been exceeded and the shell hasn't yet been launched...
            if (currentLaunchForce >= MaxLaunchForce && !isBulletFired)
            {
                // ... use the max force and launch the shell.
                currentLaunchForce = MaxLaunchForce;
                Fire();
            }
            // Otherwise, if the fire button has just started being pressed...
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                // ... reset the fired flag and reset the launch force.
                isBulletFired = false;
                currentLaunchForce = MinLaunchForce;

            }
            // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
            else if (Input.GetKey(KeyCode.Space) && !isBulletFired)
            {
                // Increment the launch force and update the slider.
                currentLaunchForce += chargeSpeed * Time.deltaTime;

                AimSlider.value = currentLaunchForce;
            }
            // Otherwise, if the fire button is released and the shell hasn't been launched yet...
            else if (Input.GetKeyUp(KeyCode.Space) && !isBulletFired)
            {
                // ... launch the shell.
                Fire();
                Camera.main.transform.GetComponent<CameraHandler>().CameraShake();
            }
        }

        private void Fire()
        {
            // Set the fired flag so only Fire is only called once.
            isBulletFired = true;

            // Create an instance of the shell and store a reference to it's rigidbody.
            GameObject bulletObject = ObjectPoolManager.Instance.SpawnObject(Bullet.gameObject);
            bulletObject.transform.position = FireBarrelTransform.position;
            bulletObject.transform.rotation = FireBarrelTransform.rotation;
            
            //Rigidbody shellInstance = Instantiate(Bullet, FireBarrelTransform.position, FireBarrelTransform.rotation) as Rigidbody;
            Rigidbody shellInstance = bulletObject.GetComponent<Rigidbody>();

            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = currentLaunchForce * FireBarrelTransform.up; ;

            // Reset the launch force.  This is a precaution in case of missing button events.
            currentLaunchForce = MinLaunchForce;

            // Play smoke effect
            ParticleSystem.Play();
        }

    }
}
