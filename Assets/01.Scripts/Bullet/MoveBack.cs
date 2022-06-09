using UnityEngine;

public class MoveBack : MonoBehaviour
{
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.back * 20 * Time.deltaTime;
    }
}
