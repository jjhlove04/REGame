using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private void Start()
    {
        empCount = itemUI.transform.Find("Count").GetComponent<TextMeshProUGUI>();

        empCount.gameObject.SetActive(true);
        empCount.text = "" + count;
    }

    private void Update()
    {
        if (useItem)
        {
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
        base.UseItem();

        if(count > 0)
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

    private void SpawnExplosion()
    {
        count--;

        empCount.text = "" + count;

        itemUI.transform.Find("Background").gameObject.SetActive(false);

        useItem = false;

        CameraManager.Instance.Shake(1, explosionLife);

        Emp();

        GameObject exp = (GameObject)Instantiate(currentDetonator, hitPoint, Quaternion.identity);

        Destroy(exp, explosionLife);
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
}
