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
        Debug.Log(objPool.gameObject);
        objPool.ReturnGameObject(this.gameObject);
    }
}