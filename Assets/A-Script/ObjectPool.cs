using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance; // Singleton : the hien

    private List<List<GameObject>> poolObjects = new List<List<GameObject>>();


    // [SerializeField] private List<int> amountToPool = new List<int>();
    [SerializeField] private List<ObjectPoolin4> listPrefabs;

    [Serializable]
    public class ObjectPoolin4
    {
        public string name;
        public int amountToPool;
        public GameObject Prefab;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        for (int i = 0; i < listPrefabs.Count; i++)
        {
            poolObjects.Add(new List<GameObject>());
        }
    }

    void Start()
    {
        for (int i = 0; i < listPrefabs.Count; i++)
        {
            for (int j = 0; j < listPrefabs[i].amountToPool; j++)
            {
                GameObject obj = Instantiate(listPrefabs[i].Prefab);
                obj.SetActive(false);
                poolObjects[i].Add(obj);
            }
        }
    }

    public GameObject GetPoolObject(string _name)
    {
        int index = 0;

        index = listPrefabs.FindIndex(poolin4 => poolin4.name == _name);

        if (index == -1)
        {
            Debug.LogError("error");
            return null;
        }

        for (int j = 0; j < poolObjects[index].Count; j++)
        {
            if (!poolObjects[index][j].activeInHierarchy)
            {
                return poolObjects[index][j];
            }
        }

        return null;
    }

    public void SetActive(float time, GameObject _gameObject)
    {
        StartCoroutine(WaitSetActive(time, _gameObject));
    }
    
    private IEnumerator WaitSetActive(float time, GameObject _gameObject)
    {
        yield return new WaitForSeconds(time);
        _gameObject.SetActive(false);
    }
}