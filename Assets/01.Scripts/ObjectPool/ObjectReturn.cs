using UnityEngine;

public class ObjectReturn : MonoBehaviour
{
    private ObjectPool objPool;

    void Awake()
    {
        objPool = ObjectPool.instacne;
    }

    private void OnDisable()
    {
        objPool.ReturnGameObject(this.gameObject);
    }
}