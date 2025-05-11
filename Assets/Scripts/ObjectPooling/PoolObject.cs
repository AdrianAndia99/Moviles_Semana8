using System.Collections.Generic;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    public virtual void OnSpawn(GameObject obj)
    {
        obj.SetActive(true);
    }

    public virtual void OnDespawn(GameObject obj)
    {
        obj.SetActive(false);
    }

    public abstract PoolObject GetObject();
    public abstract void ReturnObject(PoolObject obj);
}