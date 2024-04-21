using Assets.Scripts;
using Newtonsoft.Json;
using PimDeWitte.UnityMainThreadDispatcher;
using SocketIOClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class NetworkManager : MonoBehaviour
{
    [SerializeField]
    private InputField playerNameInput;
    [SerializeField]
    private GameObject networkPanel;
    [SerializeField]
    private PlayerController playerToSpawn;
    [SerializeField]
    private EnemySpawner enemySpawner;
    [SerializeField]
    private PlayerSpawner playerSpawner;
    [SerializeField]
    private string serverUrlLink;

    public SocketIOUnity socket;

    private static NetworkManager instance;
    public static NetworkManager Instance { get => instance; }
    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    private void Start()
    {
        serverUrlLink = "http://localhost:3000";
        var uri = new Uri(serverUrlLink);
        socket = new SocketIOUnity(uri);

        socket.Connect();

        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("socket.OnConnected");
        };

        socket.On("message", response =>
        {
            Debug.Log("Event" + response.ToString());
        });

        socket.On("enemies", OnEnemies);
        socket.On("other player connected", OnOtherPlayerConnected);
        socket.On("play", OnPlay);
        socket.On("player move", OnPlayerMove);
        socket.On("player turn", OnPlayerTurn);
        socket.On("player shoot", OnPlayerShoot);
        socket.On("health", OnHealth);
        socket.On("other player disconnected", OnOtherPlayerDisconnect);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void JoinGame()
    {
        Debug.Log("joining game");
        StartCoroutine(ConnectToServer());
    }

    #region commands

    public void CommandMove(Vector3 vec3)
    {
        string data = JsonUtility.ToJson(new PositionJSON(vec3));
        socket.Emit("player move", data);
    }

    public void CommandTurn(Quaternion quat)
    {
        string data = JsonUtility.ToJson(new RotationJSON(quat));
        socket.Emit("player turn", data);
    }

    public void CommandShoot()
    {
        Debug.Log("Shoot");
        socket.Emit("player shoot");
    }

    public void CommandHealthChange(GameObject playerFrom, GameObject playerTo, float healthChange, bool isEnemy)
    {
        print("health change cmd");
        HealthChangeJSON healthChangeJSON = new HealthChangeJSON(playerTo.name, healthChange, playerFrom.name, isEnemy);
        socket.Emit("health", JsonUtility.ToJson(healthChangeJSON));
    }

    private IEnumerator ConnectToServer()
    {
        yield return new WaitForSeconds(0.5f);

        socket.Emit("player connect");

        yield return new WaitForSeconds(1f);

        string playerName = playerNameInput.text;
        List<SpawnPoint> playerSpawnPoints = playerSpawner.PlayerSpawnPoints;
        List<SpawnPoint> enemySpawnPoints = enemySpawner.EnemySpawnPoints;
        PlayerJSON playerJSON = new PlayerJSON(playerName, playerSpawnPoints, enemySpawnPoints);
        string data = JsonUtility.ToJson(playerJSON);
        socket.Emit("play", data);
        networkPanel.SetActive(false);
    }

    #endregion

    #region Listening

    void OnEnemies(SocketIOResponse response)
    {
        string data = response.ToString();

        Debug.Log("enemies data "+ data);
        EnemiesJSON[] enemiesDataArray = JsonConvert.DeserializeObject<EnemiesJSON[]>(data);
        
        EnemiesJSON enemiesJSON = enemiesDataArray[0]; // JsonConvert.DeserializeObject<EnemiesJSON>(data);// EnemiesJSON.CreateFromJSON(data);
        //enemySpawner.SpawnEnemiesFromNetwork(enemiesJSON);

        UnityMainThreadDispatcher.Instance().Enqueue(enemySpawner.SpawnEnemyOnConnect(enemiesJSON));

    }

    void OnOtherPlayerConnected(SocketIOResponse response)
    {
        Debug.Log("Someone else joined");
        string data = response.ToString();

        UserJSON[] playerDataArray = JsonConvert.DeserializeObject<UserJSON[]>(data);
        UserJSON userJSON = playerDataArray[0];

        //Vector3 position = new Vector3(userJSON.Position[0], userJSON.Position[1], userJSON.Position[2]);
        //Quaternion rotation = Quaternion.Euler(userJSON.Rotation[0], userJSON.Rotation[1], userJSON.Rotation[2]);
        //GameObject joinedUser = GameObject.Find(userJSON.Name);
        //if (joinedUser != null)
        //{
        //    return;
        //}

        UnityMainThreadDispatcher.Instance().Enqueue(OnConnectRemoteOtherPlayer(userJSON));

        //PlayerController connectedPlayer = Instantiate<PlayerController>(playerToSpawn, position, rotation);
        ////PlayerController pc = p.GetComponent<PlayerController>();
        ////GameObject connectedPlayer = Instantiate(playerToSpawn, position, rotation);
        //// here we are setting up their other fields name and if they are local
        ////PlayerController pc = connectedPlayer.GetComponent<PlayerController>();
        //connectedPlayer.SetPlayerName(userJSON.Name);

        ////Transform t = connectedPlayer.transform.Find("HealthBar");
        ////Transform t1 = t.transform.Find("PlayerName");
        ////Text playerName = t1.GetComponent<Text>();
        ////playerName.text = userJSON.Name;
        //connectedPlayer.IsLocalPlayer = false;
        //connectedPlayer.transform.name = userJSON.Name;
        //// we also need to set the health
        //Health health = connectedPlayer.GetComponent<Health>();
        //health.CurrentHealth = userJSON.Health;
        //health.OnHealthChange();
    }

    IEnumerator OnConnectRemoteOtherPlayer(UserJSON currentUserJSON)
    {
        GameObject joinedUser = GameObject.Find(currentUserJSON.Name);
        if (joinedUser != null)
        {
            yield break ;
        }

        Vector3 position = new Vector3(currentUserJSON.Position[0], currentUserJSON.Position[1], currentUserJSON.Position[2]);
        Quaternion rotation = Quaternion.Euler(currentUserJSON.Rotation[0], currentUserJSON.Rotation[1], currentUserJSON.Rotation[2]);

        PlayerController connectedPlayer = Instantiate<PlayerController>(playerToSpawn, position, rotation);
        //PlayerController pc = p.GetComponent<PlayerController>();
        //Transform t = pc.transform.Find("HealthBar");
        //Transform t1 = t.transform.Find("PlayerName");
        //Text playerName = t1.GetComponent<Text>();
        //playerName.text = currentUserJSON.Name;
        connectedPlayer.SetPlayerName(currentUserJSON.Name);
        connectedPlayer.IsLocalPlayer = false;
        connectedPlayer.transform.name = currentUserJSON.Name;
                                                                                                                                                                                                                    
        Health health = connectedPlayer.GetComponent<Health>();
        health.CurrentHealth = currentUserJSON.Health;
        health.OnHealthChange();
        yield return null;
    }

    void OnPlay(SocketIOResponse response)
    {
        Debug.Log("you joined");
        string data = response.ToString();
        Debug.Log("data " + data);

        UserJSON[] playerDataArray = JsonConvert.DeserializeObject<UserJSON[]>(data);
        Debug.Log(playerDataArray.Length+" "+playerDataArray[0]);
        UserJSON currentUserJSON = playerDataArray[0]; // JsonConvert.DeserializeObject<UserJSON>(data);// UserJSON.CreateFromJSON(data);
        Debug.Log(" currentUserJSON "+ currentUserJSON.Name);
        Debug.Log(" currentUserJSON " + currentUserJSON.Position[0]);

        UnityMainThreadDispatcher.Instance().Enqueue(OnConnectSpawnPlayer(currentUserJSON));
        //OnConnectSpawnPlayer(currentUserJSON);
    }

    IEnumerator OnConnectSpawnPlayer(UserJSON currentUserJSON)
    {
        Vector3 position = new Vector3(currentUserJSON.Position[0], currentUserJSON.Position[1], currentUserJSON.Position[2]);
        Quaternion rotation = Quaternion.Euler(currentUserJSON.Rotation[0], currentUserJSON.Rotation[1], currentUserJSON.Rotation[2]);

        PlayerController pc = Instantiate<PlayerController>(playerToSpawn, position, rotation);
        //PlayerController pc = p.GetComponent<PlayerController>();
        //Transform t = pc.transform.Find("HealthBar");
        //Transform t1 = t.transform.Find("PlayerName");
        //Text playerName = t1.GetComponent<Text>();
        //playerName.text = currentUserJSON.Name;
        pc.SetPlayerName(currentUserJSON.Name);
        pc.IsLocalPlayer = true;
        pc.transform.name = currentUserJSON.Name;
        yield return null;
    }

    void OnPlayerMove(SocketIOResponse response)
    {
        string data = response.ToString();
        Debug.Log("data moving "+data);
        UserJSON[] playerDataArray = JsonConvert.DeserializeObject<UserJSON[]>(data);

        UserJSON userJSON = playerDataArray[0];

        UnityMainThreadDispatcher.Instance().Enqueue(OnPlayerMoveEvent(userJSON));
    }

    IEnumerator OnPlayerMoveEvent(UserJSON userJSON)
    {
        Vector3 position = new Vector3(userJSON.Position[0], userJSON.Position[1], userJSON.Position[2]);
        // if it is the current player exit
        if (userJSON.Name == playerNameInput.text)
        {
            yield break;
        }
        GameObject p = GameObject.Find(userJSON.Name);
        if (p != null)
        {
            p.transform.position = position;
        }
        yield return null;
    }

    void OnPlayerTurn(SocketIOResponse response)
    {
        string data = response.ToString();
        UserJSON[] playerDataArray = JsonConvert.DeserializeObject<UserJSON[]>(data);

        UserJSON userJSON = playerDataArray[0];

        UnityMainThreadDispatcher.Instance().Enqueue(OnRotateEvent(userJSON));
    }

    IEnumerator OnRotateEvent(UserJSON userJSON)
    {
        Quaternion rotation = Quaternion.Euler(userJSON.Rotation[0], userJSON.Rotation[1], userJSON.Rotation[2]);
        // if it is the current player exit
        if (userJSON.Name == playerNameInput.text)
        {
            yield break;
        }
        GameObject p = GameObject.Find(userJSON.Name);
        if (p != null)
        {
            p.transform.rotation = rotation;
        }
        yield return null;
    }

    void OnPlayerShoot(SocketIOResponse response)
    {
        string data = response.ToString();
        ShootJSON[] playerDataArray = JsonConvert.DeserializeObject<ShootJSON[]>(data);
        ShootJSON shootJSON = playerDataArray[0];

        UnityMainThreadDispatcher.Instance().Enqueue(OnShootEvent(shootJSON));
    }

    IEnumerator OnShootEvent(ShootJSON shootJSON)
    {
        GameObject go = GameObject.Find(shootJSON.Name);
        // instantiate the bullet etc from the player script
        PlayerController playerController = go.GetComponent<PlayerController>();
        playerController.CommandFire();
        yield return null;
    }

    void OnHealth(SocketIOResponse response)
    {
        Debug.Log("changing the health");
        // get the name of the player whose health changed
        string data = response.ToString();
        UserHealthJSON userHealthJSON = UserHealthJSON.CreateFromJSON(data);

        UnityMainThreadDispatcher.Instance().Enqueue(OnHealthEvent(userHealthJSON));
    }

    IEnumerator OnHealthEvent(UserHealthJSON userHealthJSON)
    {
        GameObject p = GameObject.Find(userHealthJSON.Name);
        Health h = p.GetComponent<Health>();
        h.CurrentHealth = userHealthJSON.Health;
        h.OnHealthChange();
        yield return null;
    }

    void OnOtherPlayerDisconnect(SocketIOResponse response)
    {
        print("user disconnected");
        string data = response.ToString();
        UserJSON[] playerDataArray = JsonConvert.DeserializeObject<UserJSON[]>(data);

        UserJSON userJSON = playerDataArray[0];
        Destroy(GameObject.Find(userJSON.Name));
    }

    #endregion

    #region JSONClasses

    [Serializable]
    public class PlayerJSON
    {
        public string Name;
        public List<PointJSON> PlayerSpawnPoints;
        public List<PointJSON> EnemySpawnPoints;

        public PlayerJSON(string _name, List<SpawnPoint> _playerSpawnPoints,
            List<SpawnPoint> _enemySpawnPoints)
        {
            this.Name = _name;
            PlayerSpawnPoints = new List<PointJSON>();
            EnemySpawnPoints = new List<PointJSON>();

            foreach(SpawnPoint point in _playerSpawnPoints)
            {
                PlayerSpawnPoints.Add(new PointJSON(point));
            }
            foreach (SpawnPoint point in _enemySpawnPoints)
            {
                EnemySpawnPoints.Add(new PointJSON(point));
            }
        }
    }

    [Serializable]
    public class PointJSON
    {
        public float[] position;
        public float[] rotation;
        public PointJSON(SpawnPoint point)
        {
            position = new float[] { point.transform.position.x,
            point.transform.position.y, point.transform.position.z };
            
            rotation = new float[] { point.transform.rotation.eulerAngles.x,
            point.transform.rotation.eulerAngles.y, point.transform.rotation.eulerAngles.z };

        }
    }

    [Serializable]
    public class PositionJSON
    {
        public float[] Position;
        public PositionJSON(Vector3 point)
        {
            Position = new float[] { point.x, point.y, point.z };
        }
    }

    [Serializable]
    public class RotationJSON
    {
        public float[] Rotation;
        public RotationJSON(Quaternion point)
        {
            Rotation = new float[] { point.eulerAngles.x, point.eulerAngles.y, point.eulerAngles.z };
        }
    }

    [Serializable]
    public class UserJSON
    {
        public string Name;
        public float Health;
        public float[] Position;
        public float[] Rotation;

        public static UserJSON CreateFromJSON(string data)
        {
            return JsonUtility.FromJson<UserJSON>(data);
        }
    }

    [Serializable]
    public class HealthChangeJSON
    {
        public string Name;
        public float HealthChange;
        public string From;
        public bool IsEnemy;

        public HealthChangeJSON(string name, float healthChange, string from, bool isEnemy)
        {
            Name = name;
            HealthChange = healthChange;
            From = from;
            IsEnemy = isEnemy;
        }
    }

    [Serializable]
    public class EnemiesJSON
    {
        public List<UserJSON> Enemies;
        public static EnemiesJSON CreateFromJSON(string data)
        {
            return JsonUtility.FromJson<EnemiesJSON>(data);
        }
    }

    [Serializable]
    public class ShootJSON
    {
        public string Name;
        public static ShootJSON CreateFromJSON(string data)
        {
            return JsonUtility.FromJson<ShootJSON>(data);
        }
    }

    [Serializable]
    public class UserHealthJSON
    {
        public string Name;
        public float Health;
        public static UserHealthJSON CreateFromJSON(string data)
        {
            return JsonUtility.FromJson<UserHealthJSON>(data);
        }
    }

    #endregion

}
