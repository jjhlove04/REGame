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


    protected Transform targetEnemy;
    private Vector3 lastMoveDir;
    protected int damage;

    protected float time;

    protected float additionalDamage = 0;

    protected bool onFurryBracelet = false;

    protected bool onPunchGun = false;

    protected bool onFMJ = false;

    protected bool onWeakLens = false;

    protected bool onTaillessPlanaria = false;

    protected bool onRedNut = false;

    protected GameObject weakLens;
    protected GameObject fMJ;
    protected GameObject furryBaracelet;
    protected GameObject punchGun;
    protected GameObject redNut;
    protected GameObject taillessPlannaria;

    void Start()
    {
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

        FindBulletEffect();
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
    }

    public ProjectileMover Create(Transform enemy, int damage)
    {
        SetTarget(enemy);
        Damage(damage);

        return this;
    }

    void SetTarget(Transform enemy)
    {
        this.targetEnemy = enemy;
    }

    public ProjectileMover SetRedNut(bool onRedNut)
    {
        print(onRedNut);

        this.onRedNut = onRedNut;

        return this;
    }

    public ProjectileMover SetTaillessPlanaria(bool onTaillessPlanaria)
    {
        print(onTaillessPlanaria);

        this.onTaillessPlanaria = onTaillessPlanaria;

        return this;
    }

    public ProjectileMover SetWeakLens(bool onWeakLens)
    {
        print(onWeakLens);

        this.onWeakLens = onWeakLens;

        return this;
    }

    public ProjectileMover SetFurryBracelet(bool onFurryBracelet, float time)
    {
        print(onFurryBracelet);

        this.onFurryBracelet = onFurryBracelet;
        this.time = time;

        return this;
    }

    public ProjectileMover SetFMJAdditionalDamage(bool FMJ,float damage)
    {
        print(FMJ);

        additionalDamage = damage;
        this.onFMJ = FMJ;

        return this;
    }

    public ProjectileMover SetOnPunchGun(bool onPunchGun)
    {
        print(onPunchGun);

        this.onPunchGun = onPunchGun;

        return this;
    }

    private void OnPunchGun(Collider other)
    {
        if (onPunchGun)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Rigidbody>().AddForce(new Vector3(15, 0, 0));
            }
        }
    }

    public void Damage(int damage)
    {
        this.damage = damage;
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
    }
}
