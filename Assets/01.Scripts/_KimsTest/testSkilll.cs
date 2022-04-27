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


    void Start()
    {
        CameraManager cameraManager = CameraManager.Instance;


        upgradeBtn = GameObject.Find("UP");

        this.gameObject.TryGetComponent(out Button btnm);
        this.gameObject.TryGetComponent(out Image image);

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

            testScriptts.Instance.turPos = count;


            if (!onTurret)
            {
                selectType = testScriptts.Instance.turType;

                if (selectType != -1)
                {
                    switch (selectType)
                    {
                        case 0:
                            TestTurretDataBase.Instance.floor = floor;
                            break;
                        case 1:
                            TestTurretDataBase.Instance.floor1 = floor;
                            break;
                        case 2:
                            TestTurretDataBase.Instance.floor2 = floor;
                            break;
                        case 3:
                            TestTurretDataBase.Instance.floor3 = floor;
                            break;
                        case 4:
                            TestTurretDataBase.Instance.floor4 = floor;
                            break;
                        default:
                            break;
                    }

                    if (GameManager.Instance.goldAmount >= GameManager.Instance.turretPtice)
                    {
                        InGameUI._instance.selectType = selectType;


                        TestTurretDataBase.Instance.Create(selectType);
                        TestTurretDataBase.Instance.createPrice += GameManager.Instance.turretPtice;
                        GameManager.Instance.turretPtice += (int)(GameManager.Instance.turretPtice * 0.2f);
                        InGameUI._instance.ShowTurPrice();

                        image.color = new Color(1, 1, 1, 0);
                        image.transform.GetChild(0).gameObject.SetActive(false);

                        onTurret = true;
                    }

                    else
                    {
                        InGameUI._instance.warningTxt.color = new Color(1, 0.8f, 0, 1);
                        InGameUI._instance.warningTxt.text = "Not Enough Gold";
                    }
                }
                else
                {
                    testScriptts.Instance.turPos = count;
                    InGameUI._instance.OpenPresetBtn();
                }
            }

            else
            {
                switch (selectType)
                {
                    case 0:
                        TestTurretDataBase.Instance.floor = floor;
                        break;
                    case 1:
                        TestTurretDataBase.Instance.floor1 = floor;
                        break;
                    case 2:
                        TestTurretDataBase.Instance.floor2 = floor;
                        break;
                    case 3:
                        TestTurretDataBase.Instance.floor3 = floor;
                        break;
                    case 4:
                        TestTurretDataBase.Instance.floor4 = floor;
                        break;
                    default:
                        break;
                }

                InGameUI._instance.selectType = selectType;
                testScriptts.Instance.turPos = count;
                testScriptts.Instance.SelectTurret();
            }


            isTest = true;
        });

        upgradeBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (isTest)
            {
                switch (InGameUI._instance.selectType)
                {
                    case 0:
                        floor = TestTurretDataBase.Instance.floor;
                        break;
                    case 1:
                        floor = TestTurretDataBase.Instance.floor1;
                        break;
                    case 2:
                        floor = TestTurretDataBase.Instance.floor2;
                        break;
                    case 3:
                        floor = TestTurretDataBase.Instance.floor3;
                        break;
                    case 4:
                        floor = TestTurretDataBase.Instance.floor4;
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
