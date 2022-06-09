using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager instance;
    public static ResourceManager Instance { get { return instance; }}


    //기차 관련 데이터 클래스
    public class PlayerData
    {
        private int exp;
        private int level;
        private int gold;
        private int tp;
        private int clearBestWave;

        public PlayerData(int exp, int level, int gold, int tp, int clearBestWave)
        {
            this.exp = exp;
            this.level = level;
            this.gold = gold;
            this.tp = tp;
            this.clearBestWave = clearBestWave;
        }

        public void SetData(int exp, int level, int gold, int tp, int clearBestWave)
        {
            this.exp = exp;
            this.level = level;
            this.gold = gold;
            this.tp = tp;
            this.clearBestWave = clearBestWave;
        }

        public void SetExp(int exp)
        {
            this.exp = exp;
        }

        public void SetLevel(int level)
        {
            this.level = level;
        }

        public void SetGold(int gold)
        {
            this.gold = gold;
        }

        public void SetTp(int tp)
        {
            this.tp = tp;
        }

        public void SetClearBestWave(int clearBestWave)
        {
            this.clearBestWave = clearBestWave;
        }

        public int GetExp()
        {
            return exp;
        }

        public int GetLevel()
        {
            return level;
        }

        public int GetGold()
        {
            return gold;
        }

        public int GetTp()
        {
            return tp;
        }

        public int GetClearBestWave()
        {
            return clearBestWave;
        }
    }

    public class TrainData
    {
        private float hp;
        private int traincar;

        public TrainData(float hp, int traincar)
        {
            this.hp = hp;
            this.traincar = traincar;
        }

        public void SetData(float hp, int traincar)
        {
            this.hp = hp;
            this.traincar = traincar;
        }

        public float GetHp()
        {
            return hp;
        }

        public int Gettraincar()
        {
            return traincar;
        }
    }

    //포탑 관련 데이터 클래스
    public class TurretData
    {
        List<int> turretUpgrade = new List<int>();

        public TurretData(List<int> turretUpgrade)
        {
            this.turretUpgrade = turretUpgrade;
        }

        public void TurretUpgrade(int num)
        {
            turretUpgrade.Add(num);
        }

        public List<int> GetTurretupgrade()
        {
            return turretUpgrade;
        }
    }

    private int first = 0;

    public enum DataType
    {
        PlayerData,
        TrainData,
        TurretData
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
        SaveAll();
    }

    private void LoadAll()
    {
        Load(DataType.PlayerData);
        Load(DataType.TrainData);
        Load(DataType.TurretData);
    }

    private void SaveAll()
    {
        SaveJSONFile(DataType.PlayerData);
        SaveJSONFile(DataType.TrainData);
        SaveJSONFile(DataType.TurretData);
    }

    public PlayerData playerData;
    public TrainData trainStatData;
    public TurretData turretStatData;

    public void SaveJSONFile(DataType dataType)
    {
        switch (dataType)
        {
            case DataType.PlayerData:
                JsonData playerDataJson = JsonMapper.ToJson(playerData);
                File.WriteAllText(Application.dataPath + "/Resources/Data/ResourcesData.json", playerDataJson.ToString());
                break;
            case DataType.TrainData:
                JsonData trainStatDataJson = JsonMapper.ToJson(trainStatData);
                File.WriteAllText(Application.dataPath + "/Resources/Data/ResourcesData.json", trainStatDataJson.ToString());
                break;
            case DataType.TurretData:
                JsonData turretStatDataJson = JsonMapper.ToJson(turretStatData);
                File.WriteAllText(Application.dataPath + "/Resources/Data/ResourcesData.json", turretStatDataJson.ToString());
                break;
        }
    }

    private void Parsing(JsonData data, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.PlayerData:
                playerData = new PlayerData(int.Parse(data["exp"].ToString()),int.Parse(data["level"].ToString()), int.Parse(data["gold"].ToString()),int.Parse(data["tp"].ToString()),int.Parse(data["clearBestWave"].ToString()));
                break;
            case DataType.TrainData:
                trainStatData = new TrainData(float.Parse(data["hp"].ToString()), int.Parse(data["traincar"].ToString()));
                break;
            case DataType.TurretData:
                List<int> intlist = new List<int>();

                foreach (var item in data)
                {
                    intlist.Add(int.Parse(item.ToString()));
                }

                turretStatData = new TurretData(intlist);
                break;
        }

    }

    public void Load(DataType dataType)
    {
        string jsonString = "";
        switch (dataType)
        {
            case DataType.PlayerData:
                jsonString = File.ReadAllText(Application.persistentDataPath + "/Resources/Data/ResourcesData.json");

                break;
            case DataType.TrainData:
                jsonString = File.ReadAllText(Application.persistentDataPath + "/Resources/Data/StationData.json");
                break;
        }
        JsonData jsondata = JsonMapper.ToObject(jsonString);
        Parsing(jsondata, dataType);
    }
}
