using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TestTurretDataBasee : MonoBehaviour
{
    private static TestTurretDataBasee instance;

    public static TestTurretDataBasee Instance
    {
        get { return instance; }
    }
    public List<GameObject> curTurretType = new List<GameObject>();

    public GameObjectBool postdic = new GameObjectBool();

    public int floor;

    public GameObject towerObj;

    [Header("�����")]
    public int resultDamage = 0;
    public int resultEXP = 0;
    public int resultGold = 0;

    public int level;
    public int curTp;
    public string _nickName;

    public int round;
    public int createPrice;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void Upgrade()
    {
        floor++;
        if (floor < curTurretType.Count)
        {
            testScripttss.Instance.ChageMakeTur(curTurretType[floor]);
        }

        testScripttss.Instance.NextUpgrade();
    }

    public void Create(Image img, int count, int flor)
    {

        if (floor < curTurretType.Count)
        {
            if (GameManagerr.Instance.goldAmount >= GameManagerr.Instance.turretPtice)
            {
                testScripttss.Instance.Create(curTurretType[floor]);

                img.color = new Color(1, 1, 1, 0);
                img.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                InGameUII._instance.warningTxt.color = new Color(1, 0.8f, 0, 1);
                InGameUII._instance.warningTxt.text = "Not Enough Gold";

            }
        }
        else
        {
            testScripttss.Instance.turPos = count;
        }
    }

    public int[] GetTurretImageCount()
    {
        int[] intArr = new int[2];
        if (floor < curTurretType.Count)
        {
            intArr[0] = curTurretType[floor].GetComponent<Turret>().turImageCount;

            if (floor + 1 < curTurretType.Count)
            {
                intArr[1] = curTurretType[floor + 1].GetComponent<Turret>().turImageCount;
            }

            else
            {
                intArr[1] = 100;
            }
        }

        else
        {
            intArr[0] = 100;
            intArr[1] = 100;
        }
        return intArr;
    }
}
