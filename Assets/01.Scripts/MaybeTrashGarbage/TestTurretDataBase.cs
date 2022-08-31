using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameObjectBool : SerializableDictionary<string, bool> { };

[System.Serializable]
public class GameObjectString : SerializableDictionary<string, GameObject> { };

public class TestTurretDataBase : MonoBehaviour
{
    private static TestTurretDataBase instance;

    public static TestTurretDataBase Instance
    {
        get { return instance; }
    }
    public GameObjectString curTurretType = new GameObjectString();

    public GameObjectBool postdic = new GameObjectBool();
    public GameObjectBool postItemDic = new GameObjectBool();
    public List<TrainItem> postItemObj = new List<TrainItem>();

    [Header("°á°ú°ª")]
    public int resultDamage = 0;
    public int resultEXP = 0;
    public int resultGold = 0;
    public int killEnemy = 0;
    public int round;

    public int level;
    public int curTp;
    public string _nickName;

    public bool isfirst;

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

    public void Upgrade(int strdat, int floor)
    {
        if (floor <= curTurretType.Count)
        {
           testScripttss.Instance.ChageMakeTur(curTurretType[strdat + "-" + floor]);
        }
    }

    public void Create(Image img, int count)
    {

        testScripttss.Instance.Create(curTurretType["0-0"]);

        img.color = new Color(1, 1, 1, 0);
        img.transform.GetChild(0).gameObject.SetActive(false);

        testScripttss.Instance.turPos = count;
    }

    //public int[] GetTurretImageCount()
    //{
    //    int[] intArr = new int[2];
    //    if (floor < curTurretType.Count)
    //    {
    //        intArr[0] = curTurretType[floor].GetComponent<Turret>().turImageCount;

    //        if (floor + 1 < curTurretType.Count)
    //        {
    //            intArr[1] = curTurretType[floor + 1].GetComponent<Turret>().turImageCount;
    //        }

    //        else
    //        {
    //            intArr[1] = 100;
    //        }
    //    }

    //    else
    //    {
    //        intArr[0] = 100;
    //        intArr[1] = 100;
    //    }
    //    return intArr;
    //}
}
