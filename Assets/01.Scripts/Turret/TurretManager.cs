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
}