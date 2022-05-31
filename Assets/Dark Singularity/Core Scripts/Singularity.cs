using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class Singularity : MonoBehaviour
{
    //This is the main script which pulls the objects nearby
    [SerializeField] public float GRAVITY_PULL = 100f;
    public static float m_GravityRadius = 1f;
    public float damage;
    public float duration;

    private List<Enemy> enemylist = new List<Enemy>();

    void Awake() {
        m_GravityRadius = GetComponent<SphereCollider>().radius;

        if(GetComponent<SphereCollider>()){
            GetComponent<SphereCollider>().isTrigger = true;
        }
    }
    
    void OnTriggerStay (Collider other) {

        if(other.attachedRigidbody) {
            float gravityIntensity = -Vector3.Distance(transform.position, other.transform.position) / m_GravityRadius;
            other.attachedRigidbody.AddForce((transform.position - other.transform.position) * other.attachedRigidbody.mass * (GRAVITY_PULL - gravityIntensity) * Time.smoothDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.BlackHole(damage,duration,true);

            enemylist.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.BlackHole(damage, duration, false);

            enemylist.Remove(enemy);

        }
    }

    private void OnDisable()
    {
        foreach (var item in enemylist)
        {
            item.BlackHole(damage, duration, false);
        }
    }
}
