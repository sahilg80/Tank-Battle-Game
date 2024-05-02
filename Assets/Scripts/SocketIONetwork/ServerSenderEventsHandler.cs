using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.SocketIONetwork.EventHandlerClasses;

namespace Assets.Scripts.SocketIONetwork
{
    public class ServerSenderEventsHandler
    {
        private SocketIOUnity socket;
        public ServerSenderEventsHandler(SocketIOUnity socket)
        {
            this.socket = socket;
        }

        public void CommandMove(Vector3 position)
        {
            string data = JsonUtility.ToJson(new PositionDataJSON(position));
            socket.Emit("player move", data);
        }

        public void CommandTurn(Quaternion rotation)
        {
            string data = JsonUtility.ToJson(new RotationDataJSON(rotation));
            socket.Emit("player turn", data);
        }

        public void CommandShoot()
        {
            socket.Emit("player shoot");
        }

        public void CommandHealthChange(float updatedHealth)
        {
            HealthChangeDataJSON healthChangeJSON = new HealthChangeDataJSON()
            {
                CurrentHealth = updatedHealth,
            };
            socket.Emit("health", JsonUtility.ToJson(healthChangeJSON));
        }

        public void CommandKilled(int kills)
        {
            PlayingUserKill playingUserKill = new PlayingUserKill()
            {
                Kills = kills
            };
            socket.Emit("killed", JsonUtility.ToJson(playingUserKill));
        }

        public void CommandSelfDied()
        {
            socket.Emit("died self");
        }

    }
}
