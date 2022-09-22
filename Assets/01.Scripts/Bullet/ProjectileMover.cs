using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public GameObject hit;
    public GameObject flash;
    public GameObject[] Detached;

    // 이동
    public float moveSpeed = 50;

    private float theSoleCandydamage;

    protected float hemostaticDamage;

    public GameObject hemostaticParticle;


    protected Transform targetEnemy;
    private Vector3 lastMoveDir;
    private int damage;
    protected int Damage
    {
        get 
        {
            if (onTheSoleCandyDamage)
            {
                return damage+ (int)Mathf.Round(OnTheSoleCandy());
            }
            return damage; 
        }
        set { damage = value; } 
    }

    protected float time;

    protected float additionalDamage = 0;

    protected bool onFurryBracelet = false;

    protected bool onPunchGun = false;

    protected bool onFMJ = false;

    protected bool onWeakLens = false;

    protected bool onTaillessPlanaria = false;

    protected bool onRedNut = false;

    protected bool onTheSoleCandyDamage = false;

    protected bool onHemostatic = false;

    protected bool onSixthGuitarString = false;
    private float sixthGuitarStringDamage = 0;

    [SerializeField]
    private GameObject sixthGuitarStringObj;

    protected GameObject weakLens;
    protected GameObject fMJ;
    protected GameObject furryBaracelet;
    protected GameObject punchGun;
    protected GameObject redNut;
    protected GameObject taillessPlannaria;
    protected GameObject theSoleCandyDamage;
    protected GameObject hemostatic;

    protected Turret turret;

    private bool check = false;

    Camera cam;

    private void OnEnable()
    {
        check = false;
    }

    void Start()
    {
        cam = Camera.main;

        if (flash != null)
        {
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
        Destroy(gameObject,5);
    }

    void FixedUpdate ()
    {
        Vector3 moveDir;
        if (targetEnemy != null && !targetEnemy.GetComponent<Enemy>().isDying)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }

        else
        {
            moveDir = lastMoveDir;

            if (!check && turret!=null)
            {
                check = true;

                turret.EnemyMissing();
            }
        }

        if (targetEnemy == null)
        {
            Vector3 mos = Input.mousePosition;
            mos.z = cam.farClipPlane;
            Vector3 ray = cam.ScreenToWorldPoint(mos);
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, ray, out hit, mos.z))
            {
                moveDir = hit.point - transform.position;
            }
        }

        // 이동
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        //회전값 적용
        transform.LookAt(targetEnemy);
    }

    private void FindBulletEffect()
    {
        weakLens = transform.Find("WeakLens").gameObject;
        fMJ = transform.Find("FMJ").gameObject;
        furryBaracelet = transform.Find("FurryBaracelet").gameObject;
        punchGun = transform.Find("PunchGun").gameObject;
        redNut = transform.Find("RedNut").gameObject;
        taillessPlannaria = transform.Find("TaillessPlannaria").gameObject;
        theSoleCandyDamage = transform.Find("TheSoleCandy").gameObject;
        hemostatic = transform.Find("Hemostatic").gameObject;

        OnBulletEffect();
    }

    private void OnBulletEffect()
    {
        weakLens.SetActive(onWeakLens);
        fMJ.SetActive(onFMJ);
        furryBaracelet.SetActive(onFurryBracelet);
        punchGun.SetActive(onPunchGun);
        redNut.SetActive(onRedNut);
        taillessPlannaria.SetActive(onTaillessPlanaria);
        theSoleCandyDamage.SetActive(onTheSoleCandyDamage);
        hemostatic.SetActive(onHemostatic);
    }

    public ProjectileMover Create(Transform enemy, int damage)
    {
        SetTarget(enemy);
        SetDamage(damage);

        return this;
    }

    void SetTarget(Transform enemy)
    {
        this.targetEnemy = enemy;
    }

    public ProjectileMover SetRedNut(bool onRedNut)
    {
        this.onRedNut = onRedNut;

        return this;
    }

    public ProjectileMover SetTaillessPlanaria(bool onTaillessPlanaria)
    {
        this.onTaillessPlanaria = onTaillessPlanaria;

        return this;
    }

    public ProjectileMover SetWeakLens(bool onWeakLens)
    {
        this.onWeakLens = onWeakLens;

        return this;
    }

    public ProjectileMover SetFurryBracelet(bool onFurryBracelet, float time)
    {
        this.onFurryBracelet = onFurryBracelet;
        this.time = time;

        return this;
    }

    public ProjectileMover SetFMJAdditionalDamage(bool FMJ,float damage)
    {
        additionalDamage = damage;
        this.onFMJ = FMJ;

        return this;
    }

    public ProjectileMover SetOnPunchGun(bool onPunchGun)
    {
        this.onPunchGun = onPunchGun;

        return this;
    }

    public ProjectileMover SetOnTheSoleCandy(bool onTheSoleCandyDamage, float damage)
    {
        this.onTheSoleCandyDamage = onTheSoleCandyDamage;

        theSoleCandydamage = damage;

        return this;
    }

    public ProjectileMover SetOnHemostatic(bool OnHemostatic, float hemostaticDamage)
    {
        this.onHemostatic = OnHemostatic;

        this.hemostaticDamage = hemostaticDamage;

        return this;
    }

    public ProjectileMover ThisTurret(Turret turret)
    {
        this.turret = turret;

        FindBulletEffect();

        return this;
    }

    private float OnTheSoleCandy()
    {
        return (Damage * theSoleCandydamage);
    }


    private void OnPunchGun(Collider other)
    {
        if (onPunchGun)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Rigidbody>().velocity= new Vector3(30,0,0);
            }
        }
    }

    public ProjectileMover SetOnSixthGuitarString(bool on, float damage)
    {
        onSixthGuitarString = on;

        sixthGuitarStringDamage = damage;

        return this;
    }

    private void SixthGuitarString()
    {
        if (onSixthGuitarString)
        {
            GameObject obj = ObjectPool.instacne.GetObject(sixthGuitarStringObj);

            obj.transform.position = targetEnemy.transform.position;

            obj.GetComponent<SixthGuitarStringProjectileMover>().Create(targetEnemy,damage, sixthGuitarStringDamage);
        }
    }

    public void SetDamage(int damage)
    {
        this.Damage = damage;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (hit != null)
        {
            var hitInstance = Instantiate(hit);
            hitInstance.transform.position = transform.position + new Vector3(2, 0, 0);

            var hitPs = hitInstance.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance, hitPs.main.duration);
            }
            else
            {
                var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitPsParts.main.duration);
            }
        }
        foreach (var detachedPrefab in Detached)
        {
            if (detachedPrefab != null)
            {
                detachedPrefab.transform.parent = null;
            }
        }
        gameObject.SetActive(false);
        //DamageText.Create(targetEnemy.position, damage,new Color(1,42/255,42/255));ㄴ

        OnPunchGun(other);

        SixthGuitarString();
    }
}