using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerBlackHole : Tower
{
    public GameObject currentDetonator;
    //private int _currentExpIdx = -1;
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
    private float spawnTime = 0;

    private float spawncurTime = 0;

    private bool spawnBomb = false;

    [SerializeField]
    private GameObject targetAreaObj;

    private Vector3 hitPoint;

    [SerializeField]
    private float attackArea = 50;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float blackHoleDuration = 3;

    InGameUII inGameUII;

    CameraManager cameraManager;

    private void Start()
    {
        //_spawnWallTime = Time.time;
        //SpawnWall();
        //if (!currentDetonator) NextExplosion();
        //else _currentExpIdx = 0;
        inGameUII = InGameUII._instance;

        targetAreaObj.transform.localScale = new Vector3(attackArea - 10, attackArea - 10, 1);

        cameraManager = CameraManager.Instance;
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

                if (Physics.Raycast(ray, out hit, 1000))
                {
                    float offsetSize = attackArea / 3;
                    hitPoint = hit.point +
                                              ((Vector3.Scale(hit.normal, new Vector3(offsetSize, offsetSize, offsetSize))));

                    targetAreaObj.transform.position = hitPoint + new Vector3(0, 30 - hitPoint.y, 0);

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
            if (spawnBomb)
            {
                spawncurTime += Time.deltaTime;

                GameObject timer = transform.Find("Timer").gameObject;

                timer.transform.position = hitPoint + new Vector3(0, 30 - hitPoint.y, 0);

                timer.SetActive(true);

                timer.GetComponent<SpriteRenderer>().material.SetFloat("_Arc1", (int)((spawncurTime / spawnTime) * 360));


                if (spawnTime <= spawncurTime)
                {
                    timer.SetActive(false);

                    inGameUII.towerActive.transform.Find("Background").gameObject.SetActive(false);

                    timer.GetComponent<SpriteRenderer>().material.SetFloat("_Arc1", (int)((spawncurTime / spawnTime) * 0));

                    spawncurTime = 0;

                    SpawnBlackHole();
                }
            }

            CoolingTime();

            targetAreaObj.SetActive(false);
        }
    }

    private void SpawnBomb()
    {
        if (!IsPointerOverUIObject())
        {
            inGameUII.towerActive.interactable = false;

            spawnBomb = true;

            coolingTower = false;

            useTower = false;

            cameraManager.OffNuclearView();
        }
    }

    private void SpawnBlackHole()
    {
        spawnBomb = false;

        targetAreaObj.SetActive(false);

        GameObject exp = (GameObject)Instantiate(currentDetonator, hitPoint, Quaternion.identity);

        exp.transform.localScale = new Vector3(attackArea/2, attackArea/2, attackArea/2);

        Destroy(exp, blackHoleDuration);

        cameraManager.Shake(blackHoleDuration, 4);
    }

    public override void UseTower()
    {
        if (useTower == true)
        {
            useTower = false;

            targetAreaObj.SetActive(false);

            inGameUII.towerActive.transform.Find("Background").gameObject.SetActive(false);

            cameraManager.OffNuclearView();
        }

        else
        {
            useTower = true;

            targetAreaObj.SetActive(true);

            inGameUII.towerActive.transform.Find("Background").gameObject.SetActive(true);

            cameraManager.OnNuclearView();
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

            inGameUII.towerActive.interactable = true;
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
