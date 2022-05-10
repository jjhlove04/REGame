using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerManager : MonoBehaviour
{
    private static TowerManager instance;

    public static TowerManager Instance
    {
        get { return instance; }
    }

    private GameObject selectTower;

    public GameObject tower;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoeded;
    }

    public void OnSceneLoeded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.name == "Main" && selectTower != null)
        {
            tower = Instantiate(selectTower, TrainManager.instance.transform.Find("TowerPos"));
        }
    }

    public void SelectTower(GameObject gameObject)
    {
        selectTower = gameObject;
    }
}