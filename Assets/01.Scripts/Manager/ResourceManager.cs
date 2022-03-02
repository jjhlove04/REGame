using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager instance;
    public static ResourceManager Instance { get { return instance; } }
    // Start is called before the first frame update

    public class StatData
    {
        public TrainStat trainStat;
        public PlayerStat playerStat;
        public TurretStat turretStat;

        public StatData()
        {

        }

        public class TrainStat
        {
            public float hp;
        }

        public class PlayerStat
        {
            public float damage; 
            public float attackSpeed;
        }

        public class TurretStat
        {
            public float hp;
            public float damage;
            public float attackSpeed;
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

    private int first = 0;

    public enum DataType
    {
        ResourceData,
        StationData
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

        //���� �Ҹ�����, �ְ� ����, �ҷ�����
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
        resources.Add(new ResourceData("Screb", 0));
        stationData = new StationData(1, 0);
        SaveAll();
    }

    private void LoadAll()
    {
        Load(DataType.ResourceData);
        Load(DataType.StationData);
    }

    private void SaveAll()
    {
        SaveJSONFile(DataType.ResourceData);
        SaveJSONFile(DataType.StationData);
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
