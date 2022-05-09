using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class testSkillScript : MonoBehaviour
{
    private GameObject upgradeBtn;
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

        CameraManager cameraManager = CameraManager.Instance;
        upgradeBtn = GameObject.Find("UP");

        gameObject.transform.GetChild(0).GetComponent<Text>().text = GameManagerr.Instance.turretPtice.ToString();

        cameraManager.TopView += LookCameraTopView;
        cameraManager.QuarterView += LookCameraQuarterView;


        btnm.onClick.AddListener(() =>
        {
            testSkillScript[] tskill = FindObjectsOfType<testSkillScript>();
            CircleTree[] cTree = FindObjectsOfType<CircleTree>();

            for (int i = 0; i < tskill.Length; i++)
            {
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
            }
            else
            {
                Debug.Log("isod");
                circleTree.transform.GetChild(0).gameObject.SetActive(true);
                circleTree.transform.GetChild(2).gameObject.SetActive(true);

                testTurData.floor = floor;

                testScripts.turPos = count;
                testScripts.SelectTurret();
            }

            isTest = true;


            clickCount++;
            if (clickCount == 2)
            {
                if (onTurret)
                {
                    testScripttss.Instance.Reload();
                }
                clickCount = 0;
            }

        });

        upgradeBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (isTest)
            {
                floor = testTurData.floor;
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
