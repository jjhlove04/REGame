using UnityEngine;

public class tesetTurret : MonoBehaviour
{
    public bool onTurret;
    private int maxBulletAmount;
    public int curBulletAmount;
    public int turType;
    void Start()
    {
        testScripttss.Instance.turretPoses.Add(this.gameObject.transform);
        testScripttss.Instance.turretData.Add(this.gameObject);
    }
}
