using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 50;

    private List<GameObject> objectPool;

    public void CreatePool()
    {
        objectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, this.transform);
            obj.SetActive(false);
            //temp
            obj.name = i.ToString();
            objectPool.Add(obj);
        }
    }

    public GameObject GetObjectFromPool(Transform _parent = null)
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                obj.GetComponent<IPoolable>().OnSpawnFromPool();
                return obj;
            }
        }

        GameObject newObj = Instantiate(prefab.gameObject);
        objectPool.Add(newObj);
        return newObj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.GetComponent<IPoolable>().OnReturnToPool();
        obj.SetActive(false);
        if (obj.transform != this.transform)
        {
            obj.transform.SetParent(this.transform);
        }
    }
}