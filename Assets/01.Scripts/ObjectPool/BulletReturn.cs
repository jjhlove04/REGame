using UnityEngine;

public class BulletReturn : MonoBehaviour
{
    private ObjectPool objPool;

    void Start()
    {
        objPool = FindObjectOfType<ObjectPool>();
    }


    private void OnDisable()
    {
        objPool.ReturnGameObject(this.gameObject);
    }
}