using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class BaitItem : Item
{
    public GameObject bait;

    [SerializeField]
    private GameObject targetAreaObj;

    [SerializeField]
    private int hp;

    public int aggroArea;

    private Vector3 hitPoint;

    [SerializeField]
    private LayerMask layerMask;

    TextMeshProUGUI baitCount;

    private Rect checkRect = new Rect(0, 0, 260, 180);

    private void Start()
    {
        baitCount = itemUI.transform.Find("Count").GetComponent<TextMeshProUGUI>();

        baitCount.gameObject.SetActive(true);
        baitCount.text = "" + count;

        targetAreaObj.transform.localScale = new Vector3(aggroArea + 15, aggroArea + 15, 1);
    }

    protected override void Update()
    {
        base.Update();

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
                        SpawnBait();
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
                useItem = true;

                targetAreaObj.SetActive(true);

                itemUI.transform.Find("Background").gameObject.SetActive(true);

                CameraManager.Instance.OnNuclearView();
            }
        }
    }

    private void SpawnBait()
    {
        if (!IsPointerOverUIObject())
        {
            count--;

            baitCount.text = "" + count;

            itemUI.transform.Find("Background").gameObject.SetActive(false);

            targetAreaObj.SetActive(false);

            useItem = false;

            GameObject exp = ObjectPool.instacne.GetObject(bait);

            exp.transform.position = hitPoint - new Vector3(0, 5, 0);
            exp.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            exp.GetComponent<Bait>().Spawn(aggroArea, hp);

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