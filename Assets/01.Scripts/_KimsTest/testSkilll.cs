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
                if(tskill[i] != this.gameObject)
                {
                    tskill[i].isTest = false;
                }
            }

            testScriptts.Instance.turPos = count;

            selectType = testScriptts.Instance.turType;

            if (selectType != -1 && GameManager.Instance.goldAmount >= GameManager.Instance.turretPtice)
            {
                InGameUI._instance.selectType = selectType;

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

                TestTurretDataBase.Instance.Create(selectType);

                GameManager.Instance.turretPtice += (int)(GameManager.Instance.turretPtice * 0.2f);

                image.color = new Color(1, 1, 1, 0);
            }
            else if (GameManager.Instance.goldAmount < 10)
            {
                InGameUI._instance.warningTxt.color = new Color(1, 0.8f, 0, 1);
                InGameUI._instance.warningTxt.text = "Not Enough Gold";
            }
            else
            {
                InGameUI._instance.OpenPresetBtn();
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
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(0, -90, 0)),Time.unscaledDeltaTime);
    }

    private void LookCameraQuarterView()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(-30, -50, 0)), Time.unscaledDeltaTime);
    }
}
