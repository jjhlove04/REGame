using UnityEngine;

public class TestDatabase : MonoBehaviour
{
    private static TestDatabase instance;

    public static TestDatabase Instance
    {
        get { return instance; }
    }

    [Header("°á°ú°ª")]
    public int resultDamage = 0;
    public int resultEXP = 0;
    public int resultGold = 0;
    public bool isRegister;


    private int level;
    public int Level
    {
        get { return level; }

        set
        {
            if (level < value)
            {
                level = value;

                curTp++;
            }
        }
    }

    public int curTp;
    public string _nickName;

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
}
