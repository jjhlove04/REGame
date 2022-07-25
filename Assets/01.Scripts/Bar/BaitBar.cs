using UnityEngine;

public class BaitBar : MonoBehaviour
{
    [SerializeField]
    private BaitHealthSystem baitHealthSystem;

    private Transform barTrm;

    private new CameraManager camera;

    private void Awake()
    {
        barTrm = transform.Find("bar");

        camera = CameraManager.Instance;
    }

    private void Start()
    {
        baitHealthSystem.OnDamaged += CallHealthSystemOnDamaged;
        baitHealthSystem.OnDied.AddListener(CallHealthSystemOnDamaged);

        CallHealthSystemOnDamaged();
    }

    private void Update()
    {
        if (camera.IsTopView())
        {
            transform.parent.rotation = Quaternion.Euler(new Vector3(90, 0, 180));
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        else
        {
            transform.parent.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            transform.rotation = Quaternion.Euler(new Vector3(315, 90, 0));
        }
    }

    void CallHealthSystemOnDamaged()
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    void UpdateBar()
    {
        barTrm.localScale = new Vector3(baitHealthSystem.GetHealthAmounetNomalized(), 1, 1);
    }

    void UpdateHealthBarVisible()
    {
        if (baitHealthSystem.IsFullHealth())
        {
            gameObject.SetActive(false);
        }

        else
        {
            gameObject.SetActive(true);
        }
    }
}
