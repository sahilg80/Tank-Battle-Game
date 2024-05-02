using Assets.Scripts.Main;
using Assets.Scripts.TankBullet;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class TankPlayerController
    {
        private TankPlayerView tankPlayerView;
        private TankPlayerScriptableObject tankPlayerSO;
        private Rigidbody playerRigidBody;
        private string id;
        private string playerName;
        private Vector3 oldPosition;
        private Quaternion oldRotation;
        private TankPlayerShooting tankPlayerShooting;
        private TankPlayerHealth tankPlayerHealth;
        private int kills;
        private Transform cameraObject;
        private bool isAlive;
        public bool IsAlive => isAlive;
        private bool isLocalPlayer;
        public bool IsLocalPlayer => isLocalPlayer;
        public string Id => id;

        public TankPlayerController(TankPlayerScriptableObject tankPlayerSO)
        {
            this.tankPlayerSO = tankPlayerSO;
            tankPlayerView = UnityEngine.Object.Instantiate<TankPlayerView>(tankPlayerSO.TankPlayerView);
            Initialize();
        }

        //public TankPlayerController(TankPlayerScriptableObject tankPlayerSO, Transform cameraObject,
        //    string tankPlayerName, string id, Vector3 position, Quaternion rotation, bool isLocalPlayer)
        //{
        //    this.tankPlayerView = UnityEngine.Object.Instantiate<TankPlayerView>(tankPlayerSO.TankPlayerView);
        //    //this.tankPlayerSO = tankPlayerSO;
        //    //this.id = id;
        //    //this.playerName = tankPlayerName;
        //    //this.isLocalPlayer = isLocalPlayer;
        //    tankPlayerView.SetCameraForBillboard(cameraObject);
        //    Initialize();
        //    //OnEnable(tankPlayerName, cameraObject, position, rotation);
        //}

        //public void SpawnTankPlayer(TankPlayerScriptableObject tankPlayerSO)
        //{
        //    this.tankPlayerSO = tankPlayerSO;
        //    if (tankPlayerView == null)
        //    {
        //        tankPlayerView = UnityEngine.Object.Instantiate<TankPlayerView>(tankPlayerSO.TankPlayerView);
        //        Initialize();
        //    }
        //}

        public void OnEnable(string tankPlayerName, string id, Transform cameraObject, int kills, float health,
            Vector3 position, Quaternion rotation, bool isLocalPlayer)
        {
            this.id = id;
            this.playerName = tankPlayerName;
            this.isLocalPlayer = isLocalPlayer;
            this.kills = kills;
            this.cameraObject = cameraObject;
            oldPosition = position;
            oldRotation = rotation;
            isAlive = true;
            tankPlayerView.SetInitialPlayerSettings(tankPlayerName, id, position, rotation);
            tankPlayerView.SetMeshColor(tankPlayerSO.MeshColorMaterial);
            tankPlayerShooting.SetLaunchForce(tankPlayerSO.LaunchForce);
            tankPlayerView.SetCameraForBillboard(cameraObject);
            if (isLocalPlayer)
            {
                UpdateKillsScoreInUI();
                CameraFollow(cameraObject);
            }
            tankPlayerHealth.SetHealth(health);
            tankPlayerView.gameObject.SetActive(true);
        }

        private void Initialize()
        {
            tankPlayerView.SetController(this);
            playerRigidBody = tankPlayerView.GetPlayerRigidBody();
            tankPlayerShooting = tankPlayerView.GetTankPlayerShoot();
            tankPlayerHealth = tankPlayerView.GetTankPlayerHealth();
        }

        private void CameraFollow(Transform cameraObject) => tankPlayerView.AlignCamera(cameraObject);
        
        public void Move(float direction)
        {
            if (Mathf.Approximately(0, direction)) return;

            //Vector3 movement = tankPlayerView.transform.forward * direction * tankPlayerSO.MovementSpeed;

            // Apply movement to the rigidbody
            //playerRigidBody.MovePosition(tankPlayerView.transform.position + movement * Time.fixedDeltaTime);
            
            playerRigidBody.velocity = tankPlayerView.transform.forward * direction * tankPlayerSO.MovementSpeed * Time.deltaTime;
            tankPlayerView.transform.position = new Vector3(tankPlayerView.transform.position.x, 0.03f, tankPlayerView.transform.position.z);

            if (Vector3.Distance(oldPosition, tankPlayerView.transform.position) > 0.01f)
            {
                GameService.Instance.ServerConnectorService.ServerSenderEvents.CommandMove(tankPlayerView.transform.position);
                oldPosition = tankPlayerView.transform.position;
            }

        }

        public void Rotate(float direction)
        {
            if (Mathf.Approximately(0, direction)) return;

            Vector3 rot = new Vector3(0, direction * tankPlayerSO.RotationSpeed * Time.deltaTime, 0);
            Quaternion deltaRot = Quaternion.Euler(rot);

            Quaternion rotation = tankPlayerView.transform.rotation * deltaRot;
            tankPlayerView.transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
            //tankPlayerView.transform.position = new Vector3(tankPlayerView.transform.position.x, 0.01f, tankPlayerView.transform.position.z);

            float angleDifference = Quaternion.Angle(oldRotation, tankPlayerView.transform.rotation);

            // Check if the angle difference is less than 0.3f
            if (angleDifference > 0.01f)
            {
                GameService.Instance.ServerConnectorService.ServerSenderEvents.CommandTurn(tankPlayerView.transform.rotation);
                oldRotation = tankPlayerView.transform.rotation;
            }
        }

        public void ShootBullet()
        {
            SetShoot();
            GameService.Instance.ServerConnectorService.ServerSenderEvents.CommandShoot();
        }

        public void UpdateConnectedTankPlayersHealth(float currentHealth)
        {
            if (isLocalPlayer)
            {
                GameService.Instance.ServerConnectorService.ServerSenderEvents.CommandHealthChange(currentHealth);
            }
        }

        public void SetPosition(Vector3 position) => tankPlayerView.transform.position = position;
        public void SetRotation(Quaternion rotation) => tankPlayerView.transform.rotation = rotation;
        public void SetShoot()
        {
            TankBulletController bulletController = GameService.Instance.TankBulletService.GetTankBullet();
            bulletController.OnEnable();
            bulletController.SetPlayerFromId(id);
            tankPlayerShooting.Fire(bulletController);
        }

        public void SetHealth(float health) => tankPlayerHealth.SetHealth(health);
        

        public void PlayerKilled(string idOfKilledFrom)
        {
            GameService.Instance.TankPlayerService.UpdatePlayerKilled(idOfKilledFrom);
            UpdateSelfDied();
        }

        private void UpdateSelfDied()
        {
            //GameService.Instance.TankPlayerService.ReturnToPool(this);
            tankPlayerView.gameObject.SetActive(false);
            isAlive = false;
            if (isLocalPlayer)
            {
                cameraObject.parent = null;
                GameService.Instance.ServerConnectorService.ServerSenderEvents.CommandSelfDied();
            }
        }

        public void UpdatePlayerKillsScore()
        {
            kills = kills + 1;
            GameService.Instance.ServerConnectorService.ServerSenderEvents.CommandKilled(kills);
        }

        public void GameOver() => tankPlayerView.ToggleInput(false);
        public void UpdateKillsScoreInUI() => GameService.Instance.UIService.GamePlayPanelUIController.UpdatePlayerKillsText(kills.ToString());

    }
}
