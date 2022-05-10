using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class testSkillScript : MonoBehaviour
{
    public GameObject circleTree;

    public int count;
    public int floor;

    [HideInInspector]
    public bool isTest;

    public bool onTurret = false;

    private testScripttss testScripts;
    private TestTurretDataBasee testTurData;
    private InGameUII inGameUI;

    public int clickCount;
    float clickTime;

    void Start()
    {
        testScripts = testScripttss.Instance;
        testTurData = TestTurretDataBasee.Instance; ;
        inGameUI = InGameUII._instance;

        gameObject.TryGetComponent(out Button btnm);
        gameObject.TryGetComponent(out Image img);

        CameraManager cameraManager = CameraManager.Instance;

        gameObject.transform.GetChild(0).GetComponent<Text>().text = GameManagerr.Instance.turretPtice.ToString();

        cameraManager.TopView += LookCameraTopView;
        cameraManager.QuarterView += LookCameraQuarterView;


        btnm.onClick.AddListener(() =>
        {
            testSkillScript[] tskill = FindObjectsOfType<testSkillScript>();
            CircleTree[] cTree = FindObjectsOfType<CircleTree>();

            for (int i = 0; i < tskill.Length; i++)
            {
                if (!onTurret)
                {
                    if (tskill[i].GetComponent<Image>().color.a != 0)
                    {
                        tskill[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    }
                }
                if (tskill[i].gameObject != this.gameObject)
                {
                    tskill[i].clickCount = 0;
                }
                if (tskill[i] != this.gameObject)
                {
                    tskill[i].isTest = false;
                }

            }


            for (int i = 0; i < cTree.Length; i++)
            {
                if(cTree[i].gameObject != circleTree)
                {
                    cTree[i].transform.GetChild(0).gameObject.SetActive(false);
                    cTree[i].transform.GetChild(1).gameObject.SetActive(false);
                    cTree[i].transform.GetChild(2).gameObject.SetActive(false);
                    cTree[i].transform.GetChild(3).gameObject.SetActive(false);
                }
            }

            testScripts.turPos = count;

            if (!onTurret)
            {
                circleTree.transform.GetChild(0).gameObject.SetActive(true);
                circleTree.transform.GetChild(1).gameObject.SetActive(true);
                circleTree.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                circleTree.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);

                img.color = new Color(1, 1, 0, 1);
            }
            else
            {
                testScripts.SelectTurret();

                //if (testScripts.turretData[count] == "asd")
                //{
                //    circleTree.transform.GetChild(0).gameObject.SetActive(true);
                //    circleTree.transform.GetChild(1).gameObject.SetActive(true);
                //}

            }

            isTest = true;

            clickCount++;
            if (clickCount == 2)
            {
                if (onTurret)
                {
                    testScripttss.Instance.Reload();
                }
                else
                {
                    circleTree.transform.GetChild(0).gameObject.SetActive(false);
                    circleTree.transform.GetChild(1).gameObject.SetActive(false);

                    img.color = new Color(1, 1, 1, 1);
                }
                clickCount = 0;
            }

        });
    }

    private void Update()
    {
        if (clickCount > 0 && onTurret)
        {
            clickTime += Time.deltaTime;
        }

        if (clickTime >= 0.51f)
        {
            clickCount = 0;
            clickTime = 0;
        }
    }
    private void LookCameraTopView()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(0, -90, 0)), Time.unscaledDeltaTime);
    }

    private void LookCameraQuarterView()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(-30, -50, 0)), Time.unscaledDeltaTime);
    }
}