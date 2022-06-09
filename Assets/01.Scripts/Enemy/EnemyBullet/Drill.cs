using UnityEngine;

public class Drill : MonoBehaviour
{
    EnemyDrill enemyDrill;
    private void Start()
    {
        enemyDrill = transform.root.GetComponent<EnemyDrill>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            TrainScript.instance.Damage(enemyDrill.GetDamage() * Time.deltaTime);
        }

        else if (other.CompareTag("Turret"))
        {
            other.GetComponent<HealthSystem>()?.Damage(enemyDrill.GetDamage() * Time.deltaTime);
        }
    }
}
