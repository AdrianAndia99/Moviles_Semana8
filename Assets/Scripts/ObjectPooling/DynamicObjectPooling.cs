using System.Collections.Generic;
using UnityEngine;

public class DinamicObjectPooling : PoolObject
{
    [SerializeField] private int initialSize = 5;

    private List<PoolObject> allObjects = new List<PoolObject>();

    void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            CreateNewObject();
        }
    }

    public override PoolObject GetObject()
    {
        foreach (var obj in allObjects)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.OnSpawn(obj.gameObject);
                return obj;
            }
        }
        return CreateNewObject();
    }
    // Método que obtiene un objeto del pool. Si existe uno inactivo, lo activa. Si no, crea uno nuevo.

    public override void ReturnObject(PoolObject obj)
    {
        obj.OnDespawn(obj.gameObject);
    }
    // Método que retorna un objeto al pool, desactivándolo.

    private PoolObject CreateNewObject()
    {
        PoolObject newObj = Instantiate(this, transform);
        newObj.OnDespawn(newObj.gameObject);
        allObjects.Add(newObj);
        return newObj;
    }
    // Método que crea un nuevo objeto, lo desactiva y lo agrega a la lista de objetos disponibles.
}