using UnityEngine;

public class TurretManager : MonoBehaviour
{
    private static TurretManager instance;

    public static TurretManager Instance
    {
        get { return instance; }
    }

    private Turret turret;

    public GameObject turrets;

    ObjectPool objectPool;

    private bool onFMJ=false;
    private int countFMJ = 0;

    private bool onFurryBracelet = false;
    private int countFurryBracelet = 0;

    private bool onOverclokcing = false;
    private int countOverclokcing = 0;

    private bool onPunchGun = false;
    private int countPunchGun = 0;

    private bool onRedNut = false;
    private int countRedNut = 0;

    private bool onTaillessPlanariaMJ = false;
    private int countTaillessPlanariaMJ = 0;

    private bool onTheSoleCandy = false;
    private int countTheSoleCandy = 0;

    private bool onWeakLens = false;
    private int countWeakLens = 0;

    private float overclokcingIncrease;

    private bool onMortarTube = false;
    private int countMortarTube = 0;   
    
    private bool onHemostatic = false;
    private int countHemostatic = 0;

    private bool onLowaMk2 = false;
    private int countLowaMk2 = 0;    
    
    private bool onSixthGuitarString = false;
    private int countSixthGuitarString = 0;    
    
    private bool onDryOil = false;
    private int countDryOil = 0;

    private bool onMachineHeart = false;
    private int countMachineHeartl = 0;    
    
    private bool onSpeedSeriesLaunches = false;
    private int countSpeedSeriesLaunches = 0;
    private GameObject speedSeriesLaunchesObj;


    private bool onShockwaveGenerator = false;
    private int countShockwaveGenerator = 0;

    private float criticalPercent=0;

    [SerializeField]
    private GameObject lowaMK23Obj;

    private GameObject shockwaveGeneratorObj;

    InGameUII inGameUII;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        turrets = gameObject;   

        instance = this;
    }

    private void Start()
    {
        objectPool = ObjectPool.instacne;
        inGameUII = InGameUII._instance;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.T) && Input.GetMouseButtonDown(0))
        {
            SelectTargetEnemy();
        }
    }

    public void SelectTurret(Turret tur)
    {
        turret = tur;
    }

    public void SelectTargetEnemy()
    {
        if(turret != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                turret.DesignateTarget(hit.point);
            }
        }
    }

    public void SpawnTurret(Turret turret)
    {
        turret
            .OnFMJ(onFMJ, countFMJ)
            .OnFurryBracelet(onFurryBracelet, countFurryBracelet)
            .Overclokcing(onOverclokcing, overclokcingIncrease, countOverclokcing)
            .OnPunchGun(onPunchGun, countPunchGun)
            .OnRedNut(onRedNut, countRedNut)
            .OnTaillessPlanaria(onTaillessPlanariaMJ, countTaillessPlanariaMJ)
            .OnTheSoleCandy(onTheSoleCandy, countTheSoleCandy)
            .OnWeakLens(onWeakLens, countWeakLens)
            .OnLowaMk23(onLowaMk2, countLowaMk2)
            .OnSixthGuitarString(onSixthGuitarString, countSixthGuitarString)
            .OnDryOil(onDryOil, countDryOil)
            .OnMachineHeart(onMachineHeart)
            .OnSpeedSeriesLaunches(onSpeedSeriesLaunches, speedSeriesLaunchesObj)
            .OnShockwaveGenerator(onShockwaveGenerator)
            .CupsAndBool(criticalPercent);
    }

    public void OnFMJ()
    {
        onFMJ = true;

        countFMJ++;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>()
                .OnFMJ(onFMJ, countFMJ);
        }
    }

    public void OnFurryBracelet()
    {
        onFurryBracelet = true;

        countFurryBracelet++;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>()
                .OnFurryBracelet(onFurryBracelet, countFurryBracelet);
        }
    }

    public void Overclokcing(float increase)
    {
        onOverclokcing = true;

        countOverclokcing++;

        overclokcingIncrease = increase;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>()
                .Overclokcing(onOverclokcing, overclokcingIncrease, countOverclokcing);
        }
    }

    public void OnPunchGun()
    {
        onPunchGun = true;

        countPunchGun++;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>()
                .OnPunchGun(onPunchGun,countPunchGun);
        }
    }

    public void OnRedNut()
    {
        onRedNut = true;

        countRedNut++;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>()
                .OnRedNut(onRedNut,countRedNut);
        }
    }

    public void OnTaillessPlanaria()
    {
        onTaillessPlanariaMJ = true;

        countTaillessPlanariaMJ++;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>()
                .OnTaillessPlanaria(onTaillessPlanariaMJ,countTaillessPlanariaMJ);
        }
    }

    public void OnTheSoleCandy()
    {
        onTheSoleCandy = true;

        countTheSoleCandy++;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>()
                .OnTheSoleCandy(onTheSoleCandy,countTheSoleCandy);
        }
    }

    public void OnWeakLens()
    {
        onWeakLens = true;

        countWeakLens++;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>()
                .OnWeakLens(onWeakLens,countWeakLens);
        }
    }
    public int OnCritical()
    {
        if (countWeakLens > 1)
        {
            return 10 + 7 * (countWeakLens - 1);
        }

        return 10;
    }
    public int CriAmount()
    {
        if (countWeakLens > 1)
        {
            return 7 * (countWeakLens - 1);
        }
        return 0;
    }

    public void MortarTube()
    {
        onMortarTube = true;

        countMortarTube++;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>()
                .OnMortarTube(onMortarTube, countMortarTube);
        }
    }

    public void Hemostatic()
    {
        onHemostatic = true;

        countHemostatic++;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>()
                .OnHemostatic(onHemostatic, countHemostatic);
        }
    }

    public void OnLowaMk23()
    {
        onLowaMk2 = true;

        countLowaMk2++;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>()
                .OnLowaMk23(onLowaMk2, countLowaMk2);
        }
    }

    public void LowaMk23(float damage,Transform targetEnemy, Vector3 Pos)
    {
        GameObject obj = objectPool.GetObject(lowaMK23Obj);

        obj.transform.position = Pos;

        obj.GetComponent<LowaMk23Projectile>().Create(damage*2, targetEnemy);
    }

    public void OnSixthGuitarString()
    {
        onSixthGuitarString = true;

        countSixthGuitarString++;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>()
                .OnSixthGuitarString(onSixthGuitarString, countSixthGuitarString);
        }
    }

    public void OnDryOil()
    {
        onDryOil = true;

        countDryOil++;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>()
                .OnDryOil(onDryOil, countDryOil);
        }
    }

    public void OnMachineHeart()
    {
        onMachineHeart = true;

        countMachineHeartl++;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>()
                .OnMachineHeart(onMachineHeart);
        }

        TrainScript.instance.OnMachineHeart(onMachineHeart,countMachineHeartl);
    }

    public void OnSpeedSeriesLaunches(GameObject speedSeriesLaunchesObj)
    {
        onSpeedSeriesLaunches = true;

        this.speedSeriesLaunchesObj = speedSeriesLaunchesObj;

        countSpeedSeriesLaunches++;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>()
                .OnSpeedSeriesLaunches(onSpeedSeriesLaunches, speedSeriesLaunchesObj);
        }

        inGameUII.OnSpeedSeriesLaunches(onSpeedSeriesLaunches, countSpeedSeriesLaunches);
    }

    public void SpeedSeriesLaunches(bool on)
    {
        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>()
                .SpeedSeriesLaunches(on);
        }
    }


    public void OnShockwaveGenerator(GameObject shockwaveGeneratorObj)
    {
        this.shockwaveGeneratorObj = shockwaveGeneratorObj;

        onShockwaveGenerator = true;

        countShockwaveGenerator++;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>()
                .OnShockwaveGenerator(onSpeedSeriesLaunches);
        }
    }

    public void ShockwaveGenerator(Vector3 pos)
    {
        GameObject Obj = objectPool.GetObject(shockwaveGeneratorObj);
        Obj.transform.position = pos;
        Obj.GetComponent<Shockwave>().Create(inGameUII.TurretDamage*(0.9f+countShockwaveGenerator*0.1f),2*countShockwaveGenerator);
    }

    public void OnShockwaveGenerator(float criticalPercent)
    {
        this.criticalPercent = criticalPercent;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>()
                .CupsAndBool(criticalPercent);
        }
    }
}