using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class TankPlayerBillBoard : MonoBehaviour
    {
        private Transform cameraObject;
        public void SetCameraObject(Transform camera) => cameraObject = camera;

        private void Update()
        {
            if (cameraObject != null)
            {
                transform.LookAt(cameraObject);
            }
        }
    }
}
