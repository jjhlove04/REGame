using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleTree : MonoBehaviour
{
    public RectTransform installBtn;
    // Start is called before the first frame update

    private TestTurretDataBase testturretdatabase;
    private GameManager gameManager;
    private InGameUII inGameUII;
    private testScripttss testscriptts;

    public int count;

    [HideInInspector]
    public bool autoReload;
    void Start()
    {
        gameManager = GameManager.Instance;
        inGameUII = InGameUII._instance;
        testscriptts = testScripttss.Instance;
        testturretdatabase = TestTurretDataBase.Instance;

        CameraManager cameraManager = CameraManager.Instance;
        cameraManager.TopView += LookCameraTopView;
        cameraManager.QuarterView += LookCameraQuarterView;


        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }


        gameObject.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            if(installBtn.GetComponent<testSkillScript>().onTurret)
            {
                levelTurret();
            }
            else
            {
                if (gameManager.goldAmount >= gameManager.turretPtice)
                {
                    return;
                }
                else
                {
                    inGameUII.warningtxt.color = new Color(1, 0.8f, 0, 1);
                    inGameUII.warningIcon.color = new Color(1, 0.8f, 0, 1);
                    inGameUII.GoldWarning.alpha = 1;
                    inGameUII.warningtxt.GetComponent<Text>().text = "Not Enough Gold";
                }
            }
            //if (!installBtn.GetComponent<testSkillScript>().onTurret)
            //{
            //    if (gameManager.goldAmount >= gameManager.turretPtice)
            //    {
            //        if (installBtn.GetComponent<testSkillScript>().floor == -1)
            //        {
            //            installBtn.GetComponent<testSkillScript>().onTurret = true;

            //            gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);

            //            testturretdatabase.Create(installBtn.GetComponent<Image>(), installBtn.GetComponent<testSkillScript>().count);

            //            installBtn.GetComponent<testSkillScript>().floor += 2;

            //            if (!testturretdatabase.curTurretType.ContainsKey("1-1"))
            //            {
            //                gameObject.transform.GetChild(1).GetChild(1).GetComponent<Button>().interactable = false;
            //                return;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        inGameUII.warningtxt.color = new Color(1, 0.8f, 0, 1);
            //        inGameUII.warningIcon.color = new Color(1, 0.8f, 0, 1);
            //        inGameUII.GoldWarning.alpha = 1;
            //        inGameUII.warningtxt.GetComponent<Text>().text = "Not Enough Gold";
            //    }
            //}
            //else
            //{
            //    levelTurret();
            //}
        });

        //gameObject.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    if (testScripttss.Instance.turretData[installBtn.GetComponent<testSkillScript>().count].name == "Base Level0-0")
        //    {
        //        CircleUpgrade(1);
        //    }
        //    else if(testScripttss.Instance.turretData[installBtn.GetComponent<testSkillScript>().count].name == "Base Level2-1")
        //    {
        //        CircleUpgrade(2);
        //    }

        //});
        //gameObject.transform.GetChild(2).GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    if (testScripttss.Instance.turretData[installBtn.GetComponent<testSkillScript>().count].name == "Base Level0-0")
        //    {
        //        CircleUpgrade(2);
        //    }
        //    else if (testScripttss.Instance.turretData[installBtn.GetComponent<testSkillScript>().count].name == "Base Level2-1")
        //    else if (testScripttss.Instance.turretData[installBtn.GetComponent<testSkillScript>().count].name == "Base Level2-1")
        //    {
        //        CircleUpgrade(3);
        //    }

        //});

        gameObject.transform.GetChild(3).GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            CircleUpgrade(1);
            ChoseTree(1);
        });
        gameObject.transform.GetChild(3).GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
        {
            CircleUpgrade(2);

            ChoseTree(2);
        });
        gameObject.transform.GetChild(3).GetChild(3).GetComponent<Button>().onClick.AddListener(() =>
        {
            CircleUpgrade(3);

            ChoseTree(3);
        });

        gameObject.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            testscriptts.Despawn();
            DesTurret();
        });
        gameObject.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            testscriptts.Despawn();
            DesTurret();
        });
        gameObject.transform.GetChild(3).GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            testscriptts.Despawn();
            DesTurret(); 
        });
    }

    private void levelTurret()
    {
        if (testscriptts.turretData[installBtn.GetComponent<testSkillScript>().count].name.Substring(10, 1) == "0")
        {
            if (testscriptts.turretData[installBtn.GetComponent<testSkillScript>().count].name == "baseTurret0-0")
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(3).gameObject.SetActive(true);

                if (!testturretdatabase.curTurretType.ContainsKey("1-2"))
                {
                    gameObject.transform.GetChild(3).GetChild(1).GetComponent<Button>().interactable = false;
                }

                if (!testturretdatabase.curTurretType.ContainsKey("2-2"))
                {
                    gameObject.transform.GetChild(3).GetChild(2).GetComponent<Button>().interactable = false;
                }

                if (!testturretdatabase.curTurretType.ContainsKey("3-2"))
                {
                    gameObject.transform.GetChild(3).GetChild(3).GetComponent<Button>().interactable = false;
                }
            }

            CircleUpgrade(1);

            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (testscriptts.turretData[installBtn.GetComponent<testSkillScript>().count].name.Substring(10, 1) == "1")
        {
            CircleUpgrade(1);
            ChoseTree(1);
        }
        else if (testscriptts.turretData[installBtn.GetComponent<testSkillScript>().count].name.Substring(10, 1) == "2")
        {
            CircleUpgrade(2);
            ChoseTree(1);
        }
        else if (testscriptts.turretData[installBtn.GetComponent<testSkillScript>().count].name.Substring(10, 1) == "3")
        {
            CircleUpgrade(3);
            ChoseTree(1);
        }
    }

    private void CircleUpgrade(int num)
    {
        testturretdatabase.Upgrade(num, installBtn.GetComponent<testSkillScript>().floor);
        installBtn.GetComponent<testSkillScript>().floor++;
    }

    private void DesTurret()
    {
        installBtn.GetComponent<testSkillScript>().onTurret = false;
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(3).gameObject.SetActive(false);

        gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);

        installBtn.GetComponent<testSkillScript>().floor = -1;
        gameObject.transform.GetChild(1).GetChild(1).GetComponent<Button>().interactable = true;
        installBtn.GetComponent<Image>().color = new Color(1,1,0,1);
        installBtn.GetComponent<Image>().transform.GetChild(0).gameObject.SetActive(true);
    }

    private void ChoseTree(int num)
    {

        gameObject.transform.GetChild(3).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);

        if (!testturretdatabase.curTurretType.ContainsKey(num + "-" + installBtn.GetComponent<testSkillScript>().floor))
        {
            gameObject.transform.GetChild(1).GetChild(1).GetComponent<Button>().interactable = false;
        }
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
