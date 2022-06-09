using UnityEngine;

public class Fire : MonoBehaviour
{
    EnemyFireAttack enemyFireAttack;
    private void Start()
    {
        enemyFireAttack = transform.root.GetComponent<EnemyFireAttack>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Train"))
        {
            TrainScript.instance.Damage(enemyFireAttack.GetDamage() * Time.deltaTime);
        }

        else if(other.CompareTag("Turret"))
        {
            other.GetComponent<HealthSystem>()?.Damage(enemyFireAttack.GetDamage() * Time.deltaTime);
        }
    }
}