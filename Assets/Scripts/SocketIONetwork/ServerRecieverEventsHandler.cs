using Assets.Scripts.Main;
using Assets.Scripts.Player;
using Newtonsoft.Json;
using PimDeWitte.UnityMainThreadDispatcher;
using SocketIOClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.SocketIONetwork.EventHandlerClasses;

namespace Assets.Scripts.SocketIONetwork
{
    public class ServerRecieverEventsHandler
    {
        private SocketIOUnity socket;
        public ServerRecieverEventsHandler(SocketIOUnity socket)
        {
            this.socket = socket;
            Initialize();
        }

        private void Initialize()
        {

            socket.On("message", response =>
            {
                Debug.Log("Event" + response.ToString());
            });

            socket.On("other player connected", OnOtherPlayerConnected);
            socket.On("play", OnPlay);
            socket.On("update health", OnHealthAffected);
            socket.On("died", OnRespawnKilledLocalPlayer);
            socket.On("died respawn", OnRespawnRemoteKilledPlayer);
            socket.On("player move", OnPlayerMove);
            socket.On("player turn", OnPlayerRotate);
            socket.On("player shoot", OnPlayerShoot);
            socket.On("game timer", OnCountdownTimerUpdate);
            socket.On("game over", OnGameOver);
        }

        private void OnGameOver(SocketIOResponse response)
        {
            string data = response.ToString();
            Debug.Log("data " + data);

            GameOverJSON[] gameOverDataArray = JsonConvert.DeserializeObject<GameOverJSON[]>(data);

            GameOverJSON gameOverJSON = gameOverDataArray[0];
            //Debug.Log(" max ki " + gameOverJSON.TimerText);

            UnityMainThreadDispatcher.Instance().Enqueue(OnGameCompleted(gameOverJSON));
        }

        private IEnumerator OnGameCompleted(GameOverJSON gameOverJSON)
        {
            GameService.Instance.TankPlayerService.DisableConnectedPlayersInput();
            GameService.Instance.UIService.GameOverPanelUIController.GameOverDataShow(gameOverJSON);
            yield return null;
        }

        private void OnCountdownTimerUpdate(SocketIOResponse response)
        {
            string data = response.ToString();
            Debug.Log("data " + data);

            GameTimer[] playerDataArray = JsonConvert.DeserializeObject<GameTimer[]>(data);

            GameTimer gameTimerJSON = playerDataArray[0];
            Debug.Log(" timer text value " + gameTimerJSON.TimerText);

            UnityMainThreadDispatcher.Instance().Enqueue(OnTimerUpdate(gameTimerJSON));
        }

        private IEnumerator OnTimerUpdate(GameTimer gameTimerJSON)
        {
            GameService.Instance.UIService.GamePlayPanelUIController.SetCountdownTimer(gameTimerJSON);
            yield return null;
        }

        private void OnHealthAffected(SocketIOResponse response)
        {
            string data = response.ToString();
            Debug.Log("data " + data);

            HealthChangeDataJSON[] playerDataArray = JsonConvert.DeserializeObject<HealthChangeDataJSON[]>(data);

            HealthChangeDataJSON healthUserJSON = playerDataArray[0];
            Debug.Log(" health user " + healthUserJSON.Id);

            UnityMainThreadDispatcher.Instance().Enqueue(OnRemotePlayerHealthChange(healthUserJSON));
        }

        private IEnumerator OnRemotePlayerHealthChange(HealthChangeDataJSON healthUserJSON)
        {
            GameService.Instance.TankPlayerService.UpdateRemotePlayerHealth(healthUserJSON);
            yield return null;
        }

        private void OnPlay(SocketIOResponse response)
        {
            string data = response.ToString();
            Debug.Log("data " + data);

            PlayingUserJSON[] playerDataArray = JsonConvert.DeserializeObject<PlayingUserJSON[]>(data);

            PlayingUserJSON currentUserJSON = playerDataArray[0]; 
            Debug.Log(" joined user name " + currentUserJSON.Name);

            UnityMainThreadDispatcher.Instance().Enqueue(SpawnOnConnectLocalPlayer(currentUserJSON));

        }

        private IEnumerator SpawnOnConnectLocalPlayer(PlayingUserJSON currentUserJSON, bool delay = false)
        {
            Vector3 position = new Vector3(currentUserJSON.Position[0], currentUserJSON.Position[1], currentUserJSON.Position[2]);
            Quaternion rotation = Quaternion.Euler(currentUserJSON.Rotation[0], currentUserJSON.Rotation[1], currentUserJSON.Rotation[2]);

            TankPlayerController controller = GameService.Instance.TankPlayerService.SpawnConnectedTankPlayer(currentUserJSON, position, rotation, true);
            //GameService.Instance.TankPlayerService.AddToConnectedPlayers(controller);

            yield return null;
        }

        private void OnOtherPlayerConnected(SocketIOResponse response)
        {
            Debug.Log("Someone else joined");
            string data = response.ToString();

            PlayingUserJSON[] playerDataArray = JsonConvert.DeserializeObject<PlayingUserJSON[]>(data);
            PlayingUserJSON userJSON = playerDataArray[0];

            UnityMainThreadDispatcher.Instance().Enqueue(OnConnectRemoteOtherPlayer(userJSON));

        }

        private IEnumerator OnConnectRemoteOtherPlayer(PlayingUserJSON currentUserJSON)
        {
            TankPlayerController tankPlayer = GameService.Instance.TankPlayerService.GetPlayerById(currentUserJSON.Id);
            if (tankPlayer != null && tankPlayer.IsAlive)
            {
                yield break;
            }

            //GameObject joinedUser = GameObject.Find(currentUserJSON.Name);
            //if (joinedUser != null)
            //{
            //    yield break;
            //}

            GameService.Instance.UIService.GamePlayPanelUIController.SetPlayerJoinedText(currentUserJSON.Name);

            yield return new WaitForSeconds(1f);

            Vector3 position = new Vector3(currentUserJSON.Position[0], currentUserJSON.Position[1], currentUserJSON.Position[2]);
            Quaternion rotation = Quaternion.Euler(currentUserJSON.Rotation[0], currentUserJSON.Rotation[1], currentUserJSON.Rotation[2]);

            TankPlayerController controller = GameService.Instance.TankPlayerService.SpawnConnectedTankPlayer(currentUserJSON, position, rotation, false);
            //GameService.Instance.TankPlayerService.AddToConnectedPlayers(controller);
            
            // set player initial health
            yield return null;
        }

        private void OnRespawnKilledLocalPlayer(SocketIOResponse response)
        {
            string data = response.ToString();
            Debug.Log(" player respawn local " + data);
            PlayingUserJSON[] playerDataArray = JsonConvert.DeserializeObject<PlayingUserJSON[]>(data);

            PlayingUserJSON userJSON = playerDataArray[0];

            UnityMainThreadDispatcher.Instance().Enqueue(ReSpawnKilledLocalPlayer(userJSON));
        }

        private IEnumerator ReSpawnKilledLocalPlayer(PlayingUserJSON currentUserJSON)
        {
            yield return new WaitForSeconds(3f);
            
            Vector3 position = new Vector3(currentUserJSON.Position[0], currentUserJSON.Position[1], currentUserJSON.Position[2]);
            Quaternion rotation = Quaternion.Euler(currentUserJSON.Rotation[0], currentUserJSON.Rotation[1], currentUserJSON.Rotation[2]);

            GameService.Instance.TankPlayerService.SpawnConnectedTankPlayer(currentUserJSON, position, rotation, true);
            
            yield return null;
        }

        private void OnRespawnRemoteKilledPlayer(SocketIOResponse response)
        {
            string data = response.ToString();
            Debug.Log("after killing player respawn remote " + data);
            PlayingUserJSON[] playerDataArray = JsonConvert.DeserializeObject<PlayingUserJSON[]>(data);

            PlayingUserJSON userJSON = playerDataArray[0];

            UnityMainThreadDispatcher.Instance().Enqueue(RespawnRemoteKilledPlayer(userJSON));
        }

        private IEnumerator RespawnRemoteKilledPlayer(PlayingUserJSON currentUserJSON)
        {
            //TankPlayerController tankPlayer = GameService.Instance.TankPlayerService.GetPlayerById(currentUserJSON.Id);
            //if (tankPlayer != null && tankPlayer.IsAlive)
            //{
            //    yield break;
            //}
            yield return new WaitForSeconds(3f);

            Vector3 position = new Vector3(currentUserJSON.Position[0], currentUserJSON.Position[1], currentUserJSON.Position[2]);
            Quaternion rotation = Quaternion.Euler(currentUserJSON.Rotation[0], currentUserJSON.Rotation[1], currentUserJSON.Rotation[2]);

            GameService.Instance.TankPlayerService.SpawnConnectedTankPlayer(currentUserJSON, position, rotation, false);

        }

        void OnPlayerMove(SocketIOResponse response)
        {
            string data = response.ToString();
            Debug.Log("data moving " + data);
            PlayingUserJSON[] playerDataArray = JsonConvert.DeserializeObject<PlayingUserJSON[]>(data);

            PlayingUserJSON userJSON = playerDataArray[0];

            UnityMainThreadDispatcher.Instance().Enqueue(OnPlayerMoveEvent(userJSON));
        }

        IEnumerator OnPlayerMoveEvent(PlayingUserJSON userJSON)
        {
            Vector3 position = new Vector3(userJSON.Position[0], userJSON.Position[1], userJSON.Position[2]);

            GameService.Instance.TankPlayerService.PlayerMovement(position, userJSON.Id);

            yield return null;
        }

        void OnPlayerRotate(SocketIOResponse response)
        {
            string data = response.ToString();
            PlayingUserJSON[] playerDataArray = JsonConvert.DeserializeObject<PlayingUserJSON[]>(data);

            PlayingUserJSON userJSON = playerDataArray[0];

            UnityMainThreadDispatcher.Instance().Enqueue(OnRotateEvent(userJSON));
        }

        IEnumerator OnRotateEvent(PlayingUserJSON userJSON)
        {
            Quaternion rotation = Quaternion.Euler(userJSON.Rotation[0], userJSON.Rotation[1], userJSON.Rotation[2]);

            GameService.Instance.TankPlayerService.PlayerRotation(rotation, userJSON.Id);

            yield return null;
        }

        void OnPlayerShoot(SocketIOResponse response)
        {
            string data = response.ToString();
            ShootDataJSON[] playerDataArray = JsonConvert.DeserializeObject<ShootDataJSON[]>(data);
            ShootDataJSON shootJSON = playerDataArray[0];

            UnityMainThreadDispatcher.Instance().Enqueue(OnShootEvent(shootJSON));
        }

        IEnumerator OnShootEvent(ShootDataJSON shootJSON)
        {
            GameService.Instance.TankPlayerService.PlayerShoot(shootJSON.Id);
            //GameObject go = GameObject.Find(shootJSON.Name);
            //// instantiate the bullet etc from the player script
            //PlayerController playerController = go.GetComponent<PlayerController>();
            //playerController.CommandFire();
            yield return null;
        }

    }
}
