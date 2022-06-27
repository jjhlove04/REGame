using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroneItme : Item
{
    public GameObject drone;

    [SerializeField]
    private GameObject targetAreaObj;

    public int aggroArea;

    private Vector3 hitPoint;

    [SerializeField]
    private LayerMask layerMask;

    TextMeshProUGUI baitCount;

    private Rect checkRect = new Rect(0, 0, 260, 180);

    ItemManager itemManager;

    List<Item> activeItems = new List<Item>();

    private void Start()
    {
        itemManager = ItemManager.Instance;
        baitCount = itemUI.transform.Find("Count").GetComponent<TextMeshProUGUI>();

        baitCount.gameObject.SetActive(true);
        baitCount.text = "" + count;

        targetAreaObj.transform.localScale = new Vector3(aggroArea + 15, aggroArea + 15, 1);

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

        if (Input.GetMouseButtonDown(1))
        {
            UnUseItem();
        }

        if (useItem)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                float offsetSize = aggroArea / 3;
                hitPoint = hit.point +
                                          ((Vector3.Scale(hit.normal, new Vector3(offsetSize, offsetSize - 5, offsetSize))));

                targetAreaObj.transform.position = hitPoint;

                if (!checkRect.Contains(Input.mousePosition))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        SpawnDrone();
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
        if (count > 0)
        {
            if (useItem == true)
            {
                useItem = false;

                targetAreaObj.SetActive(false);

                itemUI.transform.Find("Background").gameObject.SetActive(false);

                CameraManager.Instance.OffNuclearView();
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

                CameraManager.Instance.OnNuclearView();
            }
        }
    }

    public override void UnUseItem()
    {
        base.UnUseItem();

        targetAreaObj.SetActive(false);

        itemUI.transform.Find("Background").gameObject.SetActive(false);

        CameraManager.Instance.OffNuclearView();
    }

    private void SpawnDrone()
    {
        if (!IsPointerOverUIObject())
        {
            count--;

            baitCount.text = "" + count;

            itemUI.transform.Find("Background").gameObject.SetActive(false);

            targetAreaObj.SetActive(false);

            useItem = false;

            GameObject exp = ObjectPool.instacne.GetObject(drone);

            exp.transform.position = hitPoint + new Vector3(0, 50, 0);

            CameraManager.Instance.OffNuclearView();
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
