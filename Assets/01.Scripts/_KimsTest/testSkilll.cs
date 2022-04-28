using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testSkilll : MonoBehaviour
{
    private GameObject upgradeBtn;

    [SerializeField]
    private int count;
    [SerializeField]
    private int selectType = -1;
    [SerializeField]
    private int floor;

    [HideInInspector]
    public bool isTest;

    private bool onTurret = false;

    private testScriptts testScripts;
    private TestTurretDataBase testTurData;
    private InGameUI inGameUI;


    void Start()
    {
        testScripts = testScriptts.Instance;
        testTurData = TestTurretDataBase.Instance; ;
        inGameUI = InGameUI._instance;

        gameObject.TryGetComponent(out Button btnm);
        gameObject.TryGetComponent(out Image image);

        CameraManager cameraManager = CameraManager.Instance;


        upgradeBtn = GameObject.Find("UP");



        cameraManager.TopView += LookCameraTopView;
        cameraManager.QuarterView += LookCameraQuarterView;


        btnm.onClick.AddListener(() =>
        {
            testSkilll[] tskill = FindObjectsOfType<testSkilll>();

            for (int i = 0; i < tskill.Length; i++)
            {
                if (tskill[i] != this.gameObject)
                {
                    tskill[i].isTest = false;
                }
            }

            testScripts.turPos = count;


            if (!onTurret)
            {
                selectType = testScripts.turType;

                if (selectType != -1)
                {
                    switch (selectType)
                    {
                        case 0:
                            testTurData.floor = floor;
                            break;
                        case 1:
                            testTurData.floor1 = floor;
                            break;
                        case 2:
                            testTurData.floor2 = floor;
                            break;
                        case 3:
                            testTurData.floor3 = floor;
                            break;
                        case 4:
                            testTurData.floor4 = floor;
                            break;
                        default:
                            break;
                    }

                    if (GameManager.Instance.goldAmount >= GameManager.Instance.turretPtice)
                    {
                        inGameUI.selectType = selectType;


                        testTurData.Create(selectType);
                        testTurData.createPrice += GameManager.Instance.turretPtice;
                        GameManager.Instance.turretPtice += (int)(GameManager.Instance.turretPtice * 0.2f);
                        inGameUI.ShowTurPrice();

                        image.color = new Color(1, 1, 1, 0);
                        image.transform.GetChild(0).gameObject.SetActive(false);

                        onTurret = true;
                    }

                    else
                    {
                        inGameUI.warningTxt.color = new Color(1, 0.8f, 0, 1);
                        inGameUI.warningTxt.text = "Not Enough Gold";
                    }
                }
                else
                {
                    testScripts.turPos = count;
                    inGameUI.OpenPresetBtn();
                }
            }

            else
            {
                switch (selectType)
                {
                    case 0:
                        testTurData.floor = floor;
                        break;
                    case 1:
                        testTurData.floor1 = floor;
                        break;
                    case 2:
                        testTurData.floor2 = floor;
                        break;
                    case 3:
                        testTurData.floor3 = floor;
                        break;
                    case 4:
                        testTurData.floor4 = floor;
                        break;
                    default:
                        break;
                }

                inGameUI.selectType = selectType;
                testScripts.turPos = count;
                testScripts.SelectTurret();
            }


            isTest = true;
        });

        upgradeBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (isTest)
            {
                switch (inGameUI.selectType)
                {
                    case 0:
                        floor = testTurData.floor;
                        break;
                    case 1:
                        floor = testTurData.floor1;
                        break;
                    case 2:
                        floor = testTurData.floor2;
                        break;
                    case 3:
                        floor = testTurData.floor3;
                        break;
                    case 4:
                        floor = testTurData.floor4;
                        break;
                    default:
                        break;
                }
            }
        });
    }

    private void LookCameraTopView()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(0, -90, 0)), Time.unscaledDeltaTime);
    }

    private void LookCameraQuarterView()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(30, 50, 0)), Time.unscaledDeltaTime);
    }
}
