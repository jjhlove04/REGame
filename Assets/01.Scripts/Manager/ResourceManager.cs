using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager instance;
    public static ResourceManager Instance { get { return instance; } }
    public class StatData
    {
        public TrainStatData trainStatData;
        public TowerStatData towerStatData;
        public TurretStatData turretStatData;

        public StatData()
        {
            /*TrainStatData = new TrainStatData();
            playerStat = new PlayerStat();
            TurretStatData = new TurretStatData();*/
        }

        //기차 관련 데이터 클래스
        public class TrainStatData
        {
            public float hp;

            public TrainStatData(float hp)
            {
                this.hp = hp;
            }
        }

        //타워 관련 데이터 클래스
        public class TowerStatData
        {
            public float damage; 
            public float attackSpeed;

            public TowerStatData(float damage, float attackSpeed)
            {
                this.damage = damage;
                this.attackSpeed = attackSpeed;
            }
        }

        //포탑 관련 데이터 클래스
        public class TurretStatData
        {
            public float hp;
            public float damage;
            public float attackSpeed;
            public TurretStatData(float hp, float damage, float attackSpeed)
            {
                this.hp = hp;
                this.damage = damage;
                this.attackSpeed = attackSpeed;
            }
        }
    }

    public class ResourceData
    {
        public string itemName;
        public int itemAmount;

        public ResourceData(string itemName, int itemAmount)
        {
            this.itemName = itemName;
            this.itemAmount = itemAmount;
        }
    }

    public class StationData
    {
        public int currentStationNum;
        public int stationClearNum;

        public StationData(int currentStationNum, int stationClearNum)
        {
            this.currentStationNum = currentStationNum;
            this.stationClearNum = stationClearNum;
        }
    }

    public List<ResourceData> resources = new List<ResourceData>();
    public StationData stationData;
    public StatData statData;

    private int first = 0;

    public enum DataType
    {
        ResourceData,
        StationData,
        StatData
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        
        first = PlayerPrefs.GetInt("First");
        if (first == 0)
        {
            Init();
            first++;
            PlayerPrefs.SetInt("First", first);
        }
        LoadAll();
    }

    private void Init()
    {
        resources.Add(new ResourceData("Gold", 0));
        resources.Add(new ResourceData("Exp", 0));
        resources.Add(new ResourceData("Tp", 0)); //테크니컬 포인트
        resources.Add(new ResourceData("Level", 1));
        SaveAll();
    }

    private void LoadAll()
    {
        Load(DataType.ResourceData);
        Load(DataType.StationData);
        Load(DataType.StatData);
    }

    private void SaveAll()
    {
        SaveJSONFile(DataType.ResourceData);
        SaveJSONFile(DataType.StationData);
        SaveJSONFile(DataType.StatData);
    }

    public void SaveJSONFile(DataType dataType)
    {
        switch (dataType)
        {
            case DataType.ResourceData:
                JsonData resourceData = JsonMapper.ToJson(resources);
                File.WriteAllText(Application.dataPath + "/Resources/Data/ResourcesData.json", resourceData.ToString());
                break;
            case DataType.StationData:
                JsonData stationData = JsonMapper.ToJson(resources);
                File.WriteAllText(Application.dataPath + "/Resources/Data/ResourcesData.json", stationData.ToString());
                break;
            case DataType.StatData:
                JsonData statData = JsonMapper.ToJson(resources);
                File.WriteAllText(Application.dataPath + "/Resources/Data/ResourcesData.json", statData.ToString());
                break;
        }
    }

    private void Parsing(JsonData data, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.ResourceData:
                for (int i = 0; i < data.Count; i++)
                {
                    resources.Add(new ResourceData(data[i]["itemName"].ToString(), int.Parse(data[i]["itemAmount"].ToString())));
                }
                break;
            case DataType.StationData:
                stationData = new StationData(int.Parse(data["currentStationNum"].ToString()), int.Parse(data["stationClearNum"].ToString()));
                break;
            case DataType.StatData:
                statData = new StatData();
                break;
        }

    }

    public void Load(DataType dataType)
    {
        string jsonString = "";
        switch (dataType)
        {
            case DataType.ResourceData:
                jsonString = File.ReadAllText(Application.persistentDataPath + "/Resources/Data/ResourcesData.json");

                break;
            case DataType.StationData:
                jsonString = File.ReadAllText(Application.persistentDataPath + "/Resources/Data/StationData.json");
                break;
        }
        JsonData jsondata = JsonMapper.ToObject(jsonString);
        Parsing(jsondata,dataType);
    }
}
