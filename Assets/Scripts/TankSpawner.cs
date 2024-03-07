using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject tankPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(tankPrefab, tankPrefab.transform.position, Quaternion.identity);
    }

}
