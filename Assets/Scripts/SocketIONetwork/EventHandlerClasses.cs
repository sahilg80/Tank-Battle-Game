using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Utilities.Enums;

namespace Assets.Scripts.SocketIONetwork
{
    public class EventHandlerClasses
    {
        [Serializable]
        public class TankPlayerJSON
        {
            public string Name;
            public TankPlayerType TankType;
            public List<PointLocationDataJSON> PlayerSpawnPoints;

            public TankPlayerJSON(string name, TankPlayerType type, List<Transform> tankPlayerSpawnPoints)
            {
                this.Name = name;
                this.TankType = type;
                PlayerSpawnPoints = new List<PointLocationDataJSON>();

                foreach (Transform point in tankPlayerSpawnPoints)
                {
                    PlayerSpawnPoints.Add(new PointLocationDataJSON(point));
                }
            }
        }

        [Serializable]
        public class PointLocationDataJSON
        {
            public float[] position;
            public float[] rotation;
            public PointLocationDataJSON(Transform point)
            {
                position = new float[] { point.position.x,
            point.position.y, point.position.z };

                rotation = new float[] { point.rotation.eulerAngles.x,
            point.rotation.eulerAngles.y, point.rotation.eulerAngles.z };

            }
        }

        [Serializable]
        public class PositionDataJSON
        {
            public float[] Position;
            public PositionDataJSON(Vector3 point)
            {
                Position = new float[] { point.x, point.y, point.z };
            }
        }

        [Serializable]
        public class RotationDataJSON
        {
            public float[] Rotation;
            public RotationDataJSON(Quaternion point)
            {
                Rotation = new float[] { point.eulerAngles.x, point.eulerAngles.y, point.eulerAngles.z };
            }
        }

        [Serializable]
        public class PlayingUserJSON
        {
            public string Name;
            public string Id;
            public int Kills;
            public float Health;
            public TankPlayerType TankType;
            public float[] Position;
            public float[] Rotation;
        }

        [Serializable]
        public class PlayingUserKill
        {
            public string Id;
            public int Kills;
        }

        [Serializable]
        public class ShootDataJSON
        {
            public string Id;
            public string Name;
        }

        [Serializable]
        public class HealthChangeDataJSON
        {
            //public string Name;
            public string Id;
            public float CurrentHealth;

            //public HealthChangeDataJSON(string name, float healthChange, string id)
            //{
            //    Name = name;
            //    Id = id;
            //    CurrentHealth = healthChange;
            //}
        }

        //[Serializable]
        //public class EnemiesJSON
        //{
        //    public List<PlayingUserJSON> Enemies;
        //    public static EnemiesJSON CreateFromJSON(string data)
        //    {
        //        return JsonUtility.FromJson<EnemiesJSON>(data);
        //    }
        //}

        //[Serializable]
        //public class UserHealthJSON
        //{
        //    public string Name;
        //    public float Health;
        //    public static UserHealthJSON CreateFromJSON(string data)
        //    {
        //        return JsonUtility.FromJson<UserHealthJSON>(data);
        //    }
        //}

    }
}
