
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public List<ObjectPoolsInfo> objectPools;
    private static ObjectPoolManager instance;

    public static ObjectPoolManager Instance { get => instance; }

    private void Awake()
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
    // Start is called before the first frame update
    void Start()
    {
        objectPools = new List<ObjectPoolsInfo>();
    }


    public GameObject SpawnObject(GameObject prefab)
    {
        ObjectPoolsInfo pool = null;
        foreach(var p in objectPools)
        {
            if (p.Name == prefab.tag)
            {
                pool = p;
                break;
            }
        }
        if (pool == null)
        {
            pool = new ObjectPoolsInfo();
            pool.Name = prefab.tag;
            pool.InactiveObjects = new List<GameObject>();
            objectPools.Add(pool);
        }

        GameObject obj = null;

        if (pool.InactiveObjects.Count > 0)
        {
            obj = pool.InactiveObjects[0];
            obj.SetActive(true);
            pool.InactiveObjects.RemoveAt(0);
        }
        else
        {
            obj = GameObject.Instantiate(prefab);
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;
        }
        return obj;
    }

    public void DeSpawnObject(GameObject obj)
    {
        ObjectPoolsInfo pool = null;
        foreach (var p in objectPools)
        {
            if (p.Name == obj.tag)
            {
                pool = p;
                break;
            }
        }
        if(pool== null)
        {
            Debug.Log("pool does not exist");
            return;
        }

        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        obj.SetActive(false);
        pool.InactiveObjects.Add(obj);
    }

    public void DeSpawnObjectWithDelay(GameObject obj, float delay)
    {
        StartCoroutine(DelayInDeSpawn(obj, delay));
    }

    IEnumerator DelayInDeSpawn(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        DeSpawnObject(obj);
    }
}

public class ObjectPoolsInfo
{
    public string Name;
    public List<GameObject> InactiveObjects;
}
