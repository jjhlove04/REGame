using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;
    public static DataManager I
    {
        get
        {
            if (_instance == null)
            {
                GameObject dataManager = new GameObject();
                dataManager.name = "DataManager";
                _instance = dataManager.AddComponent<DataManager>();
            }
            return _instance;
        }
    }

    // ���̺� ������ ������ DataSet ����
    private DataSet _database;

    private void Start()
    {
        //InitDataManager();
    }
    public void InitDataManager()
    {
        _database = new DataSet("Database");

        MakeSheetDatset(_database);
#if UNITY_EDITOR
        //�����Ϳ��� ����� ���������Ʈ API ȣ��


#else
	//Android, Ios ȯ�濡�� ���� �� ���� json ���Ͽ��� ������ �޾ƿ�
        LoadJsonData(_database);
#endif
    }
    private void MakeSheetDatset(DataSet dataset)
    {
        var pass = new ClientSecrets();
        pass.ClientId = "281680360774-1rks6ked5prb8v2jt0bas0gmsn23084q.apps.googleusercontent.com";
        pass.ClientSecret = "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";

        var scopes = new string[] { SheetsService.Scope.SpreadsheetsReadonly };
        var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(pass, scopes, "unity sheet", CancellationToken.None).Result;

        var service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "test"
        });

        var request = service.Spreadsheets.Get("1x_t1ZvJoXuh9XxOYTcgW8ydrty72n8iAJ9V33kIHTmA").Execute();

        foreach (Sheet sheet in request.Sheets)
        {
            DataTable table = SendRequest(service, sheet.Properties.Title);
            dataset.Tables.Add(table);
        }

    }
    private DataTable SendRequest(SheetsService service, string sheetName)
    {
        DataTable result = null;
        bool success = true;

        try
        {
            //!A1:M�� ���������Ʈ A������ M������ �����͸� �޾ƿ��ڴٴ� �Ҹ�
            var request = service.Spreadsheets.Values.Get("1x_t1ZvJoXuh9XxOYTcgW8ydrty72n8iAJ9V33kIHTmA", sheetName + "!A1:M");
            //API ȣ��� �޾ƿ� IList ������
            var jsonObject = request.Execute().Values;
            //IList �����͸� jsonConvert �ϱ����� ����ȭ
            string jsonString = ParseSheetData(jsonObject);

            //DataTable�� ��ȯ
            result = SpreadSheetToDataTable(jsonString);
        }
        catch (Exception e)
        {
            success = false;
            Debug.LogError(e);
            string path = string.Format("JsonData/{0}", sheetName);
            //���� �߻��� ���� ��ο� �ִ� json ������ ���� ������ ������
            result = DataUtil.GetDataTable(path, sheetName);
            Debug.Log("��Ʈ �ε� ���з� ���� " + sheetName + " json ������ �ҷ���");
        }

        Debug.Log(sheetName + " ���������Ʈ �ε� " + (success ? "����" : "����"));

        result.TableName = sheetName;

        if (result != null)
        {
            //��ȯ�� ���̺��� json ���Ϸ� ����
            SaveDataToFile(result);
        }

        return result;
    }
    private DataTable SpreadSheetToDataTable(string json)
    {
        DataTable data = JsonConvert.DeserializeObject<DataTable>(json);
        return data;
    }
    private string ParseSheetData(IList<IList<object>> value)
    {
        StringBuilder jsonBuilder = new StringBuilder();

        IList<object> columns = value[0];

        jsonBuilder.Append("[");
        for (int row = 1; row < value.Count; row++)
        {
            var data = value[row];
            jsonBuilder.Append("{");
            for (int col = 0; col < data.Count; col++)
            {
                jsonBuilder.Append("\"" + columns[col] + "\"" + ":");
                jsonBuilder.Append("\"" + data[col] + "\"");
                jsonBuilder.Append(",");
            }
            jsonBuilder.Append("}");
            if (row != value.Count - 1)
                jsonBuilder.Append(",");
        }
        jsonBuilder.Append("]");
        return jsonBuilder.ToString();
    }
    private void SaveDataToFile(DataTable newTable)
    {
        //���ð��
        string JsonPath = string.Concat(Application.dataPath + "/Resources/JsonData/" + newTable.TableName + ".json"); 
        FileInfo info = new FileInfo(JsonPath);
        //���� ���� ���� üũ
        if (info.Exists)
        {
            DataTable checkTable = DataUtil.GetDataTable(info);
            //���� ���� üũ
            if (BinaryCheck<DataTable>(newTable, checkTable))
            {
                return;
            }
        }
        //json���� ����
        DataUtil.SetObjectFile(newTable.TableName, newTable);
    }
    private bool BinaryCheck<T>(T src, T target)
    {
        //�� ����� ���̳ʸ��� ��ȯ�ؼ� ��, �ٸ��� false ��ȯ
        BinaryFormatter formatter1 = new BinaryFormatter();
        MemoryStream stream1 = new MemoryStream();
        formatter1.Serialize(stream1, src);

        BinaryFormatter formatter2 = new BinaryFormatter();
        MemoryStream stream2 = new MemoryStream();
        formatter2.Serialize(stream2, target);

        byte[] srcByte = stream1.ToArray();
        byte[] tarByte = stream2.ToArray();

        if (srcByte.Length != tarByte.Length)
        {
            return false;
        }
        for (int i = 0; i < srcByte.Length; i++)
        {
            if (srcByte[i] != tarByte[i])
                return false;
        }
        return true;
    }
    
    private void LoadJsonData(DataSet dataset)
    {

        string JsonPath = string.Concat(Application.dataPath + "/Resources/JsonData/");
        DirectoryInfo info = new DirectoryInfo(JsonPath);
        foreach (FileInfo file in info.GetFiles())
        {
            //���� ��ο��� json �����ͼ� DataTable���� ��ȯ
            DataTable table = DataUtil.GetDataTable(file);
            dataset.Tables.Add(table);
        }
    }

    public string SelectTableData(string tableName, string primary, string column)
    {
        DataRow[] rows = _database.Tables[tableName].Select(string.Concat(primary, " = '", column, "'"));

        return rows[0][column].ToString();
    }
}
