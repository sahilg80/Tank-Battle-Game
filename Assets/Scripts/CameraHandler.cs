﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField]
        private float totalDuration;
        [SerializeField]
        private float magnitude;
        [SerializeField]
        private GameObject cameraObject;

        public void CameraShake()
        {
            StartCoroutine(Shake());
        }

        private IEnumerator Shake()
        {
            float timeElapsed = 0f;

            Vector3 originalPosition = cameraObject.transform.localPosition;

            while (timeElapsed <= totalDuration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                cameraObject.transform.localPosition = new Vector3(x, originalPosition.y, originalPosition.z);

                timeElapsed += Time.deltaTime;
                yield return null;
            }

            cameraObject.transform.localPosition = originalPosition;
        }
    }
}
