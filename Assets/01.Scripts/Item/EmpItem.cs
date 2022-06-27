using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class EmpItem : Item
{
    public GameObject currentDetonator;

    [SerializeField]
    private GameObject targetAreaObj;

    public float explosionLife = 2;

    public int empArea;

    public int empDuration;

    private Vector3 hitPoint;

    [SerializeField]
    private LayerMask layerMask;

    TextMeshProUGUI empCount;

    private Rect checkRect = new Rect(0, 0, 260, 180);

    ItemManager itemManager;

    CameraManager cameraManager;

    List<Item> activeItems = new List<Item>();

    private void Start()
    {
        cameraManager = CameraManager.Instance;

        itemManager = ItemManager.Instance;

        empCount = itemUI.transform.Find("Count").GetComponent<TextMeshProUGUI>();

        empCount.gameObject.SetActive(true);
        empCount.text = "" + count;

        targetAreaObj.transform.localScale = new Vector3(empArea + 15, empArea + 15, 1);
        
        foreach (var item in itemManager.gameItems)
        {
            if (item.GetComponent<Item>().itemType == ItemType.Active)
            {
                activeItems.Add(item.GetComponent<Item>());
            }
        }
    }

    protected override void Update()
    {
        base.Update();

        if (useItem)
        {
            if (Input.GetMouseButtonDown(1))
            {
                UnUseItem();
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Detonator dTemp = (Detonator)currentDetonator.GetComponent("Detonator");

            if (Physics.Raycast(ray, out hit, 1000))
            {
                float offsetSize = dTemp.size / 3;
                hitPoint = hit.point +
                                          ((Vector3.Scale(hit.normal, new Vector3(offsetSize, offsetSize, offsetSize))));

                targetAreaObj.transform.position = hitPoint + new Vector3(0, 1, 0);

                if (!checkRect.Contains(Input.mousePosition))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        SpawnExplosion();
                    }
                }
            }
        }

        else
        {
            targetAreaObj.SetActive(false);
        }
    }


    public override void UseItem()
    {
        if(count > 0)
        {
            if (useItem == true)
            {
                useItem = false;

                targetAreaObj.SetActive(false);

                itemUI.transform.Find("Background").gameObject.SetActive(false);

                cameraManager.OffNuclearView();
            }

            else
            {
                foreach (var item in activeItems)
                {
                    item.UnUseItem();
                }

                useItem = true;

                targetAreaObj.SetActive(true);

                itemUI.transform.Find("Background").gameObject.SetActive(true);

                cameraManager.OnNuclearView();
            }
        }
    }

    public override void UnUseItem()
    {
        base.UnUseItem();

        targetAreaObj.SetActive(false);

        itemUI.transform.Find("Background").gameObject.SetActive(false);

        cameraManager.OffNuclearView();
    }

    private void SpawnExplosion()
    {
        if (!IsPointerOverUIObject())
        {
            count--;

            empCount.text = "" + count;

            itemUI.transform.Find("Background").gameObject.SetActive(false);

            targetAreaObj.SetActive(false);

            useItem = false;

            cameraManager.Shake(1, explosionLife);

            Emp();

            GameObject exp = (GameObject)Instantiate(currentDetonator, hitPoint, Quaternion.identity);

            Destroy(exp, explosionLife);

            cameraManager.OffNuclearView();
        }
    }

    private void Emp()
    {
        Collider[] enemys = Physics.OverlapSphere(hitPoint, empArea, layerMask);

        foreach (var enemy in enemys)
        {
            Enemy enemyEmp = enemy.GetComponent<Enemy>();

            enemyEmp.OnEmp(empDuration);
        }
    }

    public override void GetItemUI(GameObject UI)
    {
        itemUI = UI;
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 2;
    }
}