using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTurretUpgrade : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField]
    private testSkilll[] tS;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();


            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Turret"))
            {
                for (int i = 0; i < tS.Length; i++)
                {
                    if (tS[i] != tS[hit.transform.GetComponent<Turret>().turCount])
                    {
                        tS[i].isTest = false;
                    }
                }

                int turCount = hit.transform.GetComponent<Turret>().turCount;
                int turType = hit.transform.GetComponent<Turret>().turType;

                tS[turCount].isTest = true;

                InGameUI._instance.selectType = turType;

                InGameUI._instance.upGradePanelRect.DOAnchorPosX(-200, 1.5f).SetUpdate(true);

                testScriptts.Instance.SelectTurret();

                testScriptts.Instance.turType = turType;
                testScriptts.Instance.turPos = turCount;
            }

        }
    }
}
