using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    private List<List<GameObject>> poolObjects = new List<List<GameObject>>();
    [SerializeField] private List<int> amountToPool = new List<int>();

    [SerializeField] private List<GameObject> bulletPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        for (int i = 0; i < bulletPrefab.Count; i++)
        {
            poolObjects.Add(new List<GameObject>());
        }
    }

    void Start()
    {
        for (int i = 0; i < bulletPrefab.Count; i++)
        {
            for (int j = 0; j < amountToPool[i]; j++)
            {
                GameObject obj = Instantiate(bulletPrefab[i]);
                obj.SetActive(false);
                poolObjects[i].Add(obj);
            }
        }
    }

    public GameObject GetPoolObject(int index)
    {
        
            for (int j = 0; j < poolObjects[index].Count; j++)
            {
                if (!poolObjects[index][j].activeInHierarchy)
                {
                    return poolObjects[index][j];
                }
            }
            return null;
    }
}