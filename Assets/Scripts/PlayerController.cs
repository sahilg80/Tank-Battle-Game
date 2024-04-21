using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public bool IsLocalPlayer;
        public Rigidbody rb;
        [SerializeField]
        private TankShooting tankShooting;
        [SerializeField]
        private Health health;
        [SerializeField]
        private PlayerSO playerData;
        [SerializeField]
        private TextMeshProUGUI playerNameField;
        
        Vector3 oldPosition;
        Vector3 currentPosition;
        Quaternion oldRotation;
        Quaternion currentRotation;

        private float movementDir;
        private float rotationDir;

        private void Start()
        {
            oldPosition = transform.position;
            currentPosition = transform.position;
            oldRotation = transform.rotation;
            currentRotation = transform.rotation;
            tankShooting.SetLaunchForce(playerData.LaunchForce);
        }

        private void Update()
        {
            if (!IsLocalPlayer) return;
            MovementInput();
            Move();

            RotationInput();
            Rotate();

            if(oldPosition != currentPosition)
            {
                // to do networking
                NetworkManager.Instance.CommandMove(transform.position);
                oldPosition = transform.position;
            }
            if(oldRotation != currentRotation)
            {
                // to do networking
                NetworkManager.Instance.CommandTurn(transform.rotation);
                oldRotation = transform.rotation;
            }

            Shoot();
        }


        public void SetPlayerName(string playerName)
        {
            playerNameField.text = playerName;
        }

        private void Move()
        {
            rb.velocity = transform.forward * movementDir * playerData.MovementSpeed;
            currentPosition = transform.position;
        }

        private void Rotate()
        {
            Vector3 rot = new Vector3(0, rotationDir * playerData.RotationSpeed * Time.deltaTime, 0);
            Quaternion deltaRot = Quaternion.Euler(rot);
            rb.rotation = transform.rotation * deltaRot;
            currentRotation = transform.rotation;
        }

        private void MovementInput()
        {
            movementDir = Input.GetAxis("Vertical");
        }

        private void RotationInput()
        {
            rotationDir = Input.GetAxis("Horizontal");
        }

        private void Shoot()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                // to do networking
                NetworkManager.Instance.CommandShoot();
                //CommandFire();
            }
        }

        public void CommandFire()
        {
            tankShooting.Fire();
        }

    }
}
