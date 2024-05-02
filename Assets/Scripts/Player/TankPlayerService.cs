using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Assets.Scripts.Utilities.Enums;
using static Assets.Scripts.SocketIONetwork.EventHandlerClasses;

namespace Assets.Scripts.Player
{
    public class TankPlayerService : MonoBehaviour
    {
        [SerializeField]
        private List<TankPlayerScriptableObject> tankPlayerSOList;
        [SerializeField]
        private Transform cameraObject;
        private List<TankPlayerController> connectedTankPlayers;
        private TankPlayerPool tankPlayerPool;

        private void Awake()
        {
            connectedTankPlayers = new List<TankPlayerController>();
            tankPlayerPool = new TankPlayerPool();
        }

        //public TankPlayerService(TankPlayerScriptableObject tankPlayerSO, TankPlayerView tankPlayerView, 
        //    Transform cameraObject)
        //{
        //    connectedTankPlayers = new List<TankPlayerController>();
        //    this.tankPlayerView = tankPlayerView;
        //    this.tankPlayerSOList = new List<TankPlayerScriptableObject>();
        //    this.cameraObject = cameraObject;
        //}

        public TankPlayerController SpawnConnectedTankPlayer(PlayingUserJSON currentUserJSON, Vector3 position, 
            Quaternion rotation, bool isLocalPlayer)
        {
            //TankPlayerController tankPlayerController = tankPlayerPool.GetTankPlayer();
            TankPlayerController tankPlayerController = GetPlayerById(currentUserJSON.Id);

            if (tankPlayerController == null)
            {
                TankPlayerScriptableObject tankPlayerSO = GetTankPlayerByType(currentUserJSON.TankType);
                
                tankPlayerController = new TankPlayerController(tankPlayerSO);
                //tankPlayerController.SpawnTankPlayer(tankPlayerSO);
                AddToConnectedPlayers(tankPlayerController);
            }

            tankPlayerController.OnEnable(currentUserJSON.Name, currentUserJSON.Id, cameraObject, currentUserJSON.Kills, 
                currentUserJSON.Health, position, rotation, isLocalPlayer);

            //TankPlayerController tankPlayerController = new TankPlayerController(tankPlayerSO, cameraObject,
            //    currentUserJSON.Name, currentUserJSON.Id, position, rotation, isLocalPlayer);

            
            
            return tankPlayerController;
        }

        public void UpdateRemotePlayerHealth(HealthChangeDataJSON healthChanged)
        {
            TankPlayerController tankPlayerController = GetPlayerById(healthChanged.Id);
            tankPlayerController?.SetHealth(healthChanged.CurrentHealth);
        }

        private void AddToConnectedPlayers(TankPlayerController tankPlayerController)
        {
            TankPlayerController controller = connectedTankPlayers.FirstOrDefault(t => t.Id == tankPlayerController.Id);

            if (controller == null)
                connectedTankPlayers.Add(tankPlayerController);
        }

        //public void ReturnToPool(TankPlayerController controller) => tankPlayerPool.ReturnTankPlayerToPool(controller);

        //public TankPlayerController SpawnConnectedTankPlayer(string tankPlayerName, string id,
        //    Vector3 position, Quaternion rotation, TankPlayerType tankType, bool isLocalPlayer)
        //{
        //    TankPlayerScriptableObject tankPlayerSO = GetTankPlayerByType(tankType);

        //    TankPlayerController tankPlayerController = new TankPlayerController(tankPlayerSO, cameraObject,
        //        tankPlayerName, id, position, rotation, isLocalPlayer);
        //    connectedTankPlayers.Add(tankPlayerController);
        //    return tankPlayerController;
        //}

        public void PlayerMovement(Vector3 position, string id)
        {
            TankPlayerController tankPlayer = connectedTankPlayers.FirstOrDefault(t => t.Id == id);
            tankPlayer?.SetPosition(position);
        }

        public void PlayerRotation(Quaternion rotation, string id)
        {
            TankPlayerController tankPlayer = connectedTankPlayers.FirstOrDefault(t => t.Id == id);
            tankPlayer?.SetRotation(rotation);
        }

        public void PlayerShoot(string id)
        {
            TankPlayerController tankPlayer = connectedTankPlayers.FirstOrDefault(t => t.Id == id);
            tankPlayer?.SetShoot();
        }

        //public bool IsPlayerExist(string id)
        //{
        //    TankPlayerController tankPlayer = connectedTankPlayers.FirstOrDefault(t => t.Id == id);
        //    if (tankPlayer != null) return true;
        //    return false;
        //}

        public TankPlayerController GetPlayerById(string id)
        {
            TankPlayerController tankPlayer = connectedTankPlayers.FirstOrDefault(t => t.Id == id);
            return tankPlayer;
        }

        public void UpdatePlayerKilled(string idOfKilledFrom)
        {
            TankPlayerController tankPlayer = GetPlayerById(idOfKilledFrom);
            if (tankPlayer!=null && tankPlayer.IsLocalPlayer)
            {
                tankPlayer.UpdatePlayerKillsScore();
                tankPlayer.UpdateKillsScoreInUI();
            }
        }

        public void DisableConnectedPlayersInput()
        {
            foreach(TankPlayerController controller in connectedTankPlayers)
            {
                controller.GameOver();
            }
        }

        //public TankPlayerController IsLocalPlayer(string id)
        //{
        //    TankPlayerController tankPlayer = connectedTankPlayers.FirstOrDefault(t => t.Id == id);
        //    return tankPlayer;
        //}

        //public string GetLocalPlayerId()
        //{
        //    TankPlayerController tankPlayer = connectedTankPlayers.FirstOrDefault(t => t.isLocalPlayer);
        //    return tankPlayer.Id;
        //}

        private TankPlayerScriptableObject GetTankPlayerByType(TankPlayerType type)
        {
            TankPlayerScriptableObject tankPlayerSO = tankPlayerSOList.FirstOrDefault(t => t.TankType == type);
            return tankPlayerSO;
        }

    }
}
