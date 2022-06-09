using UnityEngine;

public class TurretManager : MonoBehaviour
{
    private static TurretManager instance;

    public static TurretManager Instance
    {
        get { return instance; }
    }

    private Turret turret;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    public void SelectTurret(Turret tur)
    {
        turret = tur;
    }

    public void SelectTargetEnemy(Enemy enemy)
    {
        if(turret != null)
        {
            turret.DesignateTarget(enemy);
        }
    }
}
