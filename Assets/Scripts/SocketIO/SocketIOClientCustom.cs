using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketIOClientCustom : MonoBehaviour
{
    public SocketIOUnity socket;
    public string serverUrlLink = "http://localhost:3000";

    // Start is called before the first frame update
    void Start()
    {
        var uri = new Uri(serverUrlLink);
        socket = new SocketIOUnity(uri);


        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("socket.OnConnected");
        };


        socket.On("message", response =>
        {
            Debug.Log("Event" + response.ToString());
            Debug.Log(response.GetValue<string>());
        });


        socket.Connect();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            socket.EmitAsync("message", "Hello, server!"); // replace with your message
        }
    }

    void OnDestroy()
    {
        socket.Dispose();
    }
}
