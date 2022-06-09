using UnityEngine;

public class TestDatabase : MonoBehaviour
{
    private static TestDatabase instance;

    public static TestDatabase Instance
    {
        get { return instance; }
    }

    ResourceManager resourceManager;

    [Header("°á°ú°ª")]
    public int resultDamage = 0;
    public int resultEXP
    {
        get { return resourceManager.playerData.GetExp(); }
        set { resourceManager.playerData.SetExp(value); }
    }

    public int resultGold
    {
        get { return resourceManager.playerData.GetGold(); }
        set { resourceManager.playerData.SetGold(value); }
    }

    public bool isRegister;


    private int level;
    public int Level
    {
        get 
        {
            level = resourceManager.playerData.GetLevel();
            return level; 
        }

        set
        {
            if (level < value)
            {
                resourceManager.playerData.SetLevel(value);
                level = value;

                curTp++;
            }
        }
    }

    public int curTp
    {
        get { return resourceManager.playerData.GetTp(); }
        set { resourceManager.playerData.SetTp(value); }
    }
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

    private void Start()
    {
        resourceManager = ResourceManager.Instance;
    }
}
