using Assets.Scripts.Main;
using PimDeWitte.UnityMainThreadDispatcher;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.SocketIONetwork.EventHandlerClasses;
using static Assets.Scripts.Utilities.Enums;

namespace Assets.Scripts.SocketIONetwork
{
    public class ServerConnectorService : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> tankPlayerSpawnPoints;
        private TankPlayerType selectedTankPlayerType;
        private string serverUrlLink;
        private SocketIOUnity socket;
        private ServerRecieverEventsHandler serverRecieverEventsHandler;
        private ServerSenderEventsHandler serverSenderEventsHandler;

        public ServerSenderEventsHandler ServerSenderEvents => serverSenderEventsHandler;

        public void Initialize()
        {
            GameService.Instance.EventService.OnClickGameJoin.AddListener(OnJoinGame);
            serverUrlLink = "http://localhost:3000";
            Uri uri = new Uri(serverUrlLink);
            socket = new SocketIOUnity(uri);
            serverRecieverEventsHandler = new ServerRecieverEventsHandler(socket);
            serverSenderEventsHandler = new ServerSenderEventsHandler(socket);
            socket.OnConnected += (e, o) =>
            {
                Debug.Log("socket.OnConnected");

                UnityMainThreadDispatcher.Instance().Enqueue(ConnectToServer());
            };
        }

        private void OnDisable()
        {
            GameService.Instance.EventService.OnClickGameJoin.RemoveListener(OnJoinGame);
        }

        public void OnJoinGame()
        {
           socket.Connect();
        }

        private IEnumerator ConnectToServer()
        {
            GameService.Instance.EventService.OnPlayerGameJoined.InvokeEvent();

            yield return new WaitForSeconds(1f);
            Debug.Log("connected now taking updates");
            socket.Emit("PlayerConnect");

            yield return new WaitForSeconds(1f);

            string playerName = GameService.Instance.UIService.GetStartUIPanelController().GetInputPlayerName();

            TankPlayerJSON playerJSON = new TankPlayerJSON(playerName, selectedTankPlayerType, tankPlayerSpawnPoints);
            string data = JsonUtility.ToJson(playerJSON);
            socket.Emit("play", data);
        }

        public void SetSelectedTankPlayerType(TankPlayerType type) => selectedTankPlayerType = type;

    }
}
