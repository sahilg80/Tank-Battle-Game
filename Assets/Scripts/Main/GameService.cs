using Assets.Scripts.Events;
using Assets.Scripts.Player;
using Assets.Scripts.SocketIONetwork;
using Assets.Scripts.TankBullet;
using Assets.Scripts.UI;
using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        [Header("Player tank")]
        [SerializeField]
        private TankPlayerService tankPlayerService;
        public TankPlayerService TankPlayerService => tankPlayerService;

        [Header("UI")]
        [SerializeField]
        private UIService uiService;
        public UIService UIService => uiService;
        
        [Header("Server Connector")]
        [SerializeField]
        private ServerConnectorService serverConnectorService;
        public ServerConnectorService ServerConnectorService => serverConnectorService;

        [Header("Tank Bullet")]
        [SerializeField]
        private TankBulletScriptableObject tankBulletSO;
        [SerializeField]
        private TankBulletView tankBulletView;
        [SerializeField]
        private ParticleSystem bulletHitParticleEffect;
        private TankBulletService tankBulletService;
        public TankBulletService TankBulletService => tankBulletService;

        [Header("Camera SO")]
        [SerializeField]
        private Transform cameraObject;

        private EventService eventService;
        public EventService EventService => eventService;

        protected override void Awake()
        {
            base.Awake();
            //tankPlayerService = new TankPlayerService(tankPlayerSO, tankPlayerView, cameraObject);
            //tankPlayerService
            eventService = new EventService();
            tankBulletService = new TankBulletService(tankBulletView, bulletHitParticleEffect, tankBulletSO);
            serverConnectorService.Initialize();
            //ServerConnectorService connectorService = new ServerConnectorService();
            //StartCoroutine(connectorService.ConnectToServer(""));
        }
    }
}
