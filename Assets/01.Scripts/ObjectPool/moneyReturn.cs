using UnityEngine;
using UnityEngine.UI;

public class moneyReturn : MonoBehaviour
{
    private ObjectPool objPool;
    private float curTime;
    public float deleteTime = 1f;

    public float min;
    void Start()
    {
        objPool = FindObjectOfType<ObjectPool>();
    }
    private void Update()
    {
        deleteMoneyTxt(this.gameObject);

        curTime += Time.deltaTime;

        if (curTime >= deleteTime)
        {
            this.gameObject.SetActive(false);
            curTime = 0f;
        }

    }
    void deleteMoneyTxt(GameObject prefab)
    {
        prefab.TryGetComponent<Text>(out Text txt);
        if (txt.color.g != 0)
        {
            prefab.transform.position = new Vector3(prefab.transform.position.x, Mathf.Lerp(prefab.transform.position.y,
                InGameUII._instance.moneyPos.transform.position.y, Time.deltaTime), 0);
        }
        else
        {
            prefab.transform.position = new Vector3(prefab.transform.position.x, Mathf.Lerp(prefab.transform.position.y,
                InGameUII._instance.downMoneyPos.transform.position.y, Time.deltaTime), 0);
        }

        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, Mathf.Lerp(txt.color.a, 0f, Time.deltaTime));
    }

    private void OnDisable()
    {
        objPool.ReturnGameObject(this.gameObject);
    }
}
