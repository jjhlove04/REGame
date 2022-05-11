using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleTree : MonoBehaviour
{
    public RectTransform installBtn;
    // Start is called before the first frame update
    void Start()
    {
        CameraManager cameraManager = CameraManager.Instance;
        cameraManager.TopView += LookCameraTopView;
        cameraManager.QuarterView += LookCameraQuarterView;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }


        gameObject.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            if (GameManagerr.Instance.goldAmount >= GameManagerr.Instance.turretPtice)
            {
                if (installBtn.GetComponent<testSkillScript>().floor == -1)
                {
                    installBtn.GetComponent<testSkillScript>().onTurret = true;
                    gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    gameObject.transform.GetChild(2).gameObject.SetActive(true);

                    TestTurretDataBase.Instance.Create(installBtn.GetComponent<Image>(), installBtn.GetComponent<testSkillScript>().count);

                    installBtn.GetComponent<testSkillScript>().floor += 2;
                }
            }
            else
            {
                InGameUII._instance.warningTxt.color = new Color(1, 0.8f, 0, 1);
                InGameUII._instance.warningTxt.text = "Not Enough Gold";
            }
        });

        gameObject.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            if (testScripttss.Instance.turretData[installBtn.GetComponent<testSkillScript>().count].name == "Base Level1-1")
            {
                CircleUpgrade(1);
            }
            else if(testScripttss.Instance.turretData[installBtn.GetComponent<testSkillScript>().count].name == "Base Level2-1")
            {
                CircleUpgrade(2);
            }

        });
        gameObject.transform.GetChild(2).GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
        {
            if (testScripttss.Instance.turretData[installBtn.GetComponent<testSkillScript>().count].name == "Base Level1-1")
            {
                CircleUpgrade(2);
            }
            else if (testScripttss.Instance.turretData[installBtn.GetComponent<testSkillScript>().count].name == "Base Level2-1")
            {
                CircleUpgrade(3);
            }

        });
    }

    private void CircleUpgrade(int num)
    {
        TestTurretDataBase.Instance.Upgrade(num, installBtn.GetComponent<testSkillScript>().floor);
        installBtn.GetComponent<testSkillScript>().floor++;
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }

    private void LookCameraTopView()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(0, -90, 0)); //Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(0, -90, 0)), Time.unscaledDeltaTime);
    }

    private void LookCameraQuarterView()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(-30, -50, 0));//Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(-30, -50, 0)), Time.unscaledDeltaTime);
    }
}
