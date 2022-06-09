using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;
    TextMeshPro text;
    Color alpha;
    [HideInInspector]
    public float damage;

    public static void Create(Vector3 pos, float damage, Color color)
    {
        Transform textPrefab = Resources.Load<Transform>("DamageText");
        Transform textTrm = Instantiate(textPrefab);

        textTrm.position = pos+ new Vector3(0,10,0);
        textTrm.parent = GameObject.Find("DamageText").transform;
        textTrm.GetComponent<DamageText>().damage = damage;
        textTrm.GetComponent<DamageText>().alpha = color;
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 2.0f;
        alphaSpeed = 2.0f;
        destroyTime = 2.0f;

        text = GetComponent<TextMeshPro>();
        //text.color = alpha;
        //text.text = "-"+damage.ToString();
        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        //text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}