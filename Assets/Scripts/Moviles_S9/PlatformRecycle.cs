using UnityEngine;

public class PlatformRecycle : MonoBehaviour
{
    private Camera mainCam;
    private float despawnThreshold = 6f;
    private PoolObject poolObject;

    private void Awake()
    {
        mainCam = Camera.main;
        poolObject = GetComponent<PoolObject>();
    }

    private void Update()
    {
        if (mainCam == null || poolObject == null) return;

        if (transform.position.y < mainCam.transform.position.y - despawnThreshold)
        {
            poolObject.ReturnObject(poolObject);
        }
    }
}