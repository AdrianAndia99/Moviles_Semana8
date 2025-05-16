using System.Collections.Generic;
using UnityEngine;

public class StaticObjectPooling : PoolObject
{
    [SerializeField] private GameObject prefabPlatform;
    [SerializeField] private int poolSize;

    private Queue<PoolObject> pool = new Queue<PoolObject>();

    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            PoolObject obj = Instantiate(prefabPlatform, transform).GetComponent<PoolObject>();
            obj.OnDespawn(obj.gameObject);
            pool.Enqueue(obj);
        }
    }
    // Método que inicializa el pool con los objetos. Crea los objetos y los desactiva al principio.

    public override PoolObject GetObject()
    {
        if (pool.Count > 0)
        {
            PoolObject obj = pool.Dequeue();
            obj.OnSpawn(obj.gameObject);
            return obj;
        }

        Debug.LogWarning("Static Pool exhausted.");
        return null;
    }
    // Método que obtiene un objeto del pool. Si no hay objetos disponibles, devuelve un mensaje de advertencia.

    public override void ReturnObject(PoolObject obj)
    {
        obj.OnDespawn(obj.gameObject);
        pool.Enqueue(obj);
    }
    // Método que retorna un objeto al pool, desactivándolo.
}
