using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class TowerActiveAttack : MonoBehaviour, ITowerActiveSkill
{
    public GameObject currentDetonator;
    public GameObject bomb;
    //private int _currentExpIdx = -1;
    public GameObject[] detonatorPrefabs;
    public float bombLife = 3;
    public float explosionLife = 10;
    public float timeScale = 1.0f;
    public float detailLevel = 1.0f;

    /*public GameObject wall;
    private GameObject _currentWall;
    private float _spawnWallTime = -1000;
    private Rect _guiRect;*/

    private bool useTower = false;

    private bool coolingTower = true;

    [SerializeField]
    private float coolTime = 60;

    private float curTime = 0;

    [SerializeField]
    private GameObject targetAreaObj;

    private Vector3 hitPoint;

    [SerializeField]
    private float attackArea = 50;

    [SerializeField]
    private LayerMask layerMask;

    InGameUII inGameUII;

    private void Start()
    {
        //_spawnWallTime = Time.time;
        //SpawnWall();
        //if (!currentDetonator) NextExplosion();
        //else _currentExpIdx = 0;
        inGameUII = InGameUII._instance;

        targetAreaObj.transform.localScale = new Vector3(attackArea-5, attackArea-5, 1);
    }

    /*private void OnGUI()
    {
        _guiRect = new Rect(7, Screen.height - 180, 250, 200);
        GUILayout.BeginArea(_guiRect);

        GUILayout.BeginVertical();
        string expName = currentDetonator.name;
        if (GUILayout.Button(expName + " (Click For Next)"))
        {
            NextExplosion();
        }
        if (GUILayout.Button("Rebuild Wall"))
        {
            SpawnWall();
        }
        if (GUILayout.Button("Camera Far"))
        {
            Camera.main.transform.position = new Vector3(0, 0, -7);
            Camera.main.transform.eulerAngles = new Vector3(13.5f, 0, 0);
        }
        if (GUILayout.Button("Camera Near"))
        {
            Camera.main.transform.position = new Vector3(0, -8.664466f, 31.38269f);
            Camera.main.transform.eulerAngles = new Vector3(1.213462f, 0, 0);
        }

        GUILayout.Label("Time Scale");
        timeScale = GUILayout.HorizontalSlider(timeScale, 0.0f, 1.0f);

        GUILayout.Label("Detail Level (re-explode after change)");
        detailLevel = GUILayout.HorizontalSlider(detailLevel, 0.0f, 1.0f);

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }*/

    /*private void NextExplosion()
    {
        if (_currentExpIdx >= detonatorPrefabs.Length - 1) _currentExpIdx = 0;
        else _currentExpIdx++;
        currentDetonator = detonatorPrefabs[_currentExpIdx];
    }

    private void SpawnWall()
    {
        if (_currentWall) Destroy(_currentWall);
        _currentWall = (GameObject) Instantiate(wall, new Vector3(-7, -12, 48), Quaternion.identity);
    }*/

    //is this a bug? We can't use the same rect for placing the GUI as for checking if the mouse contains it...
    private Rect checkRect = new Rect(0, 0, 260, 180);

    private void Update()
    {
        //keeps the UI in the corner in case of resize... 
        //_guiRect = new Rect(7, Screen.height - 150, 250, 200);

        //keeps the play button from making an explosion
        if (coolingTower)
        {
            if (useTower)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                Detonator dTemp = (Detonator)currentDetonator.GetComponent("Detonator");

                if (Physics.Raycast(ray, out hit ,1000))
                {
                    float offsetSize = dTemp.size / 3;
                    hitPoint = hit.point +
                                              ((Vector3.Scale(hit.normal, new Vector3(offsetSize, offsetSize, offsetSize))));

                    targetAreaObj.transform.position = hitPoint + new Vector3(0,1 ,0);

                    if (!checkRect.Contains(Input.mousePosition))
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            SpawnBomb();
                        }
                    }
                }
            }

            else
            {
                targetAreaObj.SetActive(false);
            }
        }

        else
        {
            CoolingTime();

            targetAreaObj.SetActive(false);
        }

    }

    private void SpawnBomb()
    {
        if (!IsPointerOverUIObject())
        {
            CameraManager.Instance.Shake(bombLife, 0.75f);

            targetAreaObj.SetActive(false);

            coolingTower = false;

            useTower = false;

            inGameUII.towerActive.transform.Find("Background").gameObject.SetActive(false);

            GameObject exp = (GameObject)Instantiate(bomb, hitPoint + new Vector3(0,185,0), Quaternion.identity);

            Destroy(exp, bombLife);

            Invoke("SpawnExplosion", bombLife);
        }
    }

    private void SpawnExplosion()
    {
        CameraManager.Instance.Shake(3, explosionLife);

        Damage();

        Detonator dTemp = (Detonator)currentDetonator.GetComponent("Detonator");

        GameObject exp = (GameObject)Instantiate(currentDetonator, hitPoint, Quaternion.identity);
        dTemp = (Detonator)exp.GetComponent("Detonator");
        dTemp.detail = detailLevel;

        Destroy(exp, explosionLife);
    }

    private void Damage()
    {
        Collider[] enemys = Physics.OverlapSphere(hitPoint, attackArea,layerMask);

        foreach (var enemy in enemys)
        {
            HealthSystem healthSystem = enemy.GetComponent<HealthSystem>();
            healthSystem.Damage(1000);
        }
    }

    public void UseTower()
    {
        if (useTower == true)
        {
            useTower = false;

            targetAreaObj.SetActive(false);

            inGameUII.towerActive.transform.Find("Background").gameObject.SetActive(false);
        }

        else
        {
            useTower = true;

            targetAreaObj.SetActive(true);

            inGameUII.towerActive.transform.Find("Background").gameObject.SetActive(true);
        }

    }

    private void CoolingTime()
    {
        CoolTimeImg();

        curTime += Time.deltaTime;

        if (coolTime < curTime)
        {
            coolingTower = true;

            curTime = 0;

            inGameUII.towerActive.transform.Find("CoolTimer").GetComponent<Image>().fillAmount = 0;
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 2;
    }

    public void CoolTimeImg()
    {
        inGameUII.towerActive.transform.Find("CoolTimer").GetComponent<Image>().fillAmount = 1 - (curTime / coolTime);
    }
}
