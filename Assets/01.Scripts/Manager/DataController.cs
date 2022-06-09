using UnityEngine;

public class DataController : MonoBehaviour
{
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }

    private static DataController instance;
    public static DataController Instance
    {
        get
        {
            if (!instance)
            {
                _container = new GameObject(); _container.name = "DataConroller";
                instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }
            return instance;
        }
    }

    public string GameDataFileName = "JongseoWakeUp.json";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
