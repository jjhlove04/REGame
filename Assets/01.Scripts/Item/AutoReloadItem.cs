using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoReloadItem : Item
{
    public List<Button> selectButton = new List<Button>();

    private List<int> countArr= new List<int>();

    TextMeshProUGUI empCount;

    CircleTree circleTree;

    testScripttss testScripttss;

    private void Start()
    {
        testScripttss = testScripttss.Instance;

        empCount = itemUI.transform.Find("Count").GetComponent<TextMeshProUGUI>();

        empCount.gameObject.SetActive(true);

        empCount.text = "" + count;

        selectButton = InGameUII._instance.SetSelectReloadButton();

        foreach (var item in selectButton)
        {
            item.transform.parent.gameObject.SetActive(true);

            circleTree = item.transform.parent.parent.parent.GetComponent<CircleTree>();

            item.onClick.AddListener(() =>
            {
                CircleTree itemCircleTree = item.transform.parent.parent.parent.GetComponent<CircleTree>();

                SelectReload(itemCircleTree);

            });
        }
    }

    protected override void Update()
    {
        base.Update();

        if (useItem)
        {
            for (int i = 0; i < countArr.Count; i++)
            {
                Turret turret = testScripttss.turretData[countArr[i]].GetComponent<Turret>();

                if (turret!=null && turret.IsNeedReload())
                {
                    turret.Reload();
                }
            }
        }
    }

    public override void UseItem()
    {
        itemUI.transform.Find("Background").gameObject.SetActive(!itemUI.transform.Find("Background").gameObject.activeSelf);

        useItem = !useItem;
    }

    private void SelectReload(CircleTree circletree)
    {
        if (!circletree.autoReload)
        {
            OnReload(circletree);
            print(0);
        }

        else
        {
            OffReload(circletree);

            print(1);
        }
    }

    private void OnReload(CircleTree circletree)
    {
        if (count > 0)
        {
            Turret turret = testScripttss.turretData[circletree.count].GetComponent<Turret>();

            circletree.transform.GetChild(0).GetChild(0).Find("Background").gameObject.SetActive(true);

            countArr.Add(circletree.count);
            count--;

            circletree.autoReload = true;
        }

        empCount.text = "" + count;

    }

    private void OffReload(CircleTree circletree)
    {
        circletree.transform.GetChild(0).GetChild(0).Find("Background").gameObject.SetActive(false);

        countArr.Remove(circletree.count);
        count++;

        circletree.autoReload = false;

        empCount.text = "" + count;
    }

    public override void GetItemUI(GameObject UI)
    {
        itemUI = UI;
    }
}