using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize;

    private List<MonoBehaviour> pool = new List<MonoBehaviour>();

    public GameObject Prefab { get => prefab; }


    void Awake()
    {
        InitializePool();
    }


    public T GetObjectFromPool<T>() where T : MonoBehaviour
    {
        foreach (var obj in pool)
        {
            if (!obj.gameObject.activeSelf)
            {
                obj.gameObject.SetActive(true);
                return obj as T;
            }
        }

        return null;
    }

    public void ReturnObjectToPool(MonoBehaviour obj)
    {
        obj.gameObject.transform.SetParent(transform);
        obj.gameObject.SetActive(false);
    }


    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.transform.position = transform.position;
            obj.SetActive(false);
            pool.Add(obj.GetComponent<MonoBehaviour>());
        }
    }
}
