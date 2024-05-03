using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.Player
{
    public class TankPlayerView : MonoBehaviour
    {
        [SerializeField]
        private TankPlayerShooting tankPlayerShooting;
        [SerializeField]
        private TankPlayerHealth tankPlayerHealth;
        [SerializeField]
        private TankPlayerBillBoard tankPlayerBillBoard;

        [SerializeField]
        private Rigidbody tankPlayerRb;
        [SerializeField]
        private TextMeshProUGUI tankPlayerNameText;
        [SerializeField]
        private Transform cameraLocation;
        [SerializeField]
        private List<MeshRenderer> meshRenderers;

        private bool isInitialized;

        private float movementDir;
        private float rotationDir;
        private TankPlayerController tankPlayerController;

        public void SetController(TankPlayerController controller)
        {
            tankPlayerController = controller;
            tankPlayerHealth.SetController(controller);
        }
        //private void SetLocalPlayer(bool value) => IsLocalPlayer = value;

        public void AlignCamera(Transform cameraObject)
        {
            cameraObject.position = cameraLocation.position;
            cameraObject.rotation = cameraLocation.rotation;
            cameraObject.parent = transform;
        }

        public void SetMeshColor(Material material)
        {
            foreach(MeshRenderer mesh in meshRenderers)
            {
                mesh.material = material;
            }
        }
        public void ToggleInput(bool value) => isInitialized = value;
        public void SetCameraForBillboard(Transform cameraObject) => tankPlayerBillBoard.SetCameraObject(cameraObject);

        private void SetTankPlayerName(string value) => tankPlayerNameText.SetText(value);
        
        public void SetInitialPlayerSettings(string tankPlayerName, string id, Vector3 position, Quaternion rotation)
        {
            //SetLocalPlayer(isLocalPlayer);
            SetTankPlayerName(tankPlayerName);
            transform.name = id;
            transform.position = position;
            transform.rotation = rotation;
            isInitialized = true;
        }

        public TankPlayerShooting GetTankPlayerShoot() => tankPlayerShooting;
        public TankPlayerHealth GetTankPlayerHealth() => tankPlayerHealth;
        public Rigidbody GetPlayerRigidBody() => tankPlayerRb;

        private void Update()
        {
            if (!isInitialized || !tankPlayerController.IsLocalPlayer ) return;
            Movement();
            tankPlayerController?.Move(movementDir);

            Rotation();
            tankPlayerController?.Rotate(rotationDir);
            Shoot();
        }

        public void DestroyTankPlayer() => Destroy(gameObject);
        
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
                tankPlayerController?.ShootBullet();
            }
        }
        private void OnDisable()
        {
            isInitialized = false;
        }
    }
}
