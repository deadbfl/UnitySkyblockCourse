using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager instance;

    [SerializeField] private int size;

    private Dictionary<string, GameObject> poolingPrefabDic = new Dictionary<string, GameObject>();
    private Dictionary<string, Queue<GameObject>> poolingObjects = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        instance = this;

        var poolingObjectPrefab = Resources.LoadAll("2) Pooling", typeof(GameObject)).Cast<GameObject>().ToArray();

        for (int i = 0; i < poolingObjectPrefab.Length; i++)
        {
            GameObject prefab = poolingObjectPrefab[i];
            string key = prefab.name;

            if (!poolingPrefabDic.ContainsKey(key))
            {
                poolingPrefabDic.Add(key, prefab);
            }

            for (int j = 0; j < size; j++)
            {
                // nesne olusturma

                GameObject obj = InstantiateObjects(key);

                if (!poolingObjects.ContainsKey(key))
                {
                    Queue<GameObject> queue = new Queue<GameObject>();
                    poolingObjects.Add(key, queue);
                }

                SetObject(obj);
            }
        }
    }
    #region OpenObject
    public GameObject OpenObject(string key, Vector3 pos, Vector3 eulerAngles, Transform parent)
    {
        GameObject obj = GetObject(key);

        if (obj == null)
        {
            Debug.LogError(string.Format("{0} this key object not found", key));
            return null;
        }

        obj.transform.parent = parent;
        obj.transform.position = pos;
        obj.transform.eulerAngles = eulerAngles;
        obj.SetActive(true);

        return obj;
    }

    public GameObject OpenObject(string key, Vector3 pos)
    {
        return OpenObject(key, pos, Vector3.zero, null);
    }

    public GameObject OpenObject(string key, Vector3 pos, Vector3 rotation)
    {
        return OpenObject(key, pos, rotation, null);
    }
    public GameObject OpenObject(string key, Vector3 pos, Transform parent)
    {
        return OpenObject(key, pos, Vector3.zero, parent);
    }
    public GameObject OpenObject(string key, Transform parent)
    {
        return OpenObject(key, parent.position, Vector3.zero, parent);
    }
    public GameObject OpenObject(string key, Vector3 pos, Vector3 rotation, float time)
    {
        GameObject particleObj = OpenObject(key, pos, rotation, null);

        StartSetObjectCoroutine(particleObj, time);

        return particleObj;
    }
    #endregion
    public void StartSetObjectCoroutine(GameObject obj, float time)
    {
        StartCoroutine(SetObject(obj, time));
    }
    private IEnumerator SetObject(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);

        SetObject(obj);
    }
    private GameObject InstantiateObjects(string key)
    {
        return Instantiate(poolingPrefabDic[key]);
    }
    private GameObject GetObject(string key)
    {
        if (!poolingObjects.ContainsKey(key) || poolingObjects[key].Count < 0)
        {
            Debug.LogError(string.Format("{0} this item not found.", key));
            return null;
        }

        // Sonradan olusturma islemi tamamlandi
        if (poolingObjects[key].Count <= size / 2)
        {
            for (int j = 0; j < size; j++)
            {
                // nesne olusturma

                GameObject obj = InstantiateObjects(key);
                SetObject(obj);
            }
        }

        GameObject poolingObj = poolingObjects[key].Dequeue();
        return poolingObj;
    }
    private void SetObject(GameObject obj)
    {
        string key = obj.name;

        if (key.Length < 8)
        {
            Debug.LogError(string.Format("{0} this object not include in pooling", obj));
            return;
        }

        key = key.Remove(key.Length - 7); // (Clone) ismi silindi

        if (!poolingObjects.ContainsKey(key)) // keye ait queue yoksa
        {
            Debug.LogError(string.Format("{0} this object not include in pooling queue", key));
            return;
        }

        poolingObjects[key].Enqueue(obj);

        obj.transform.parent = transform;
        obj.SetActive(false);
    }
}
