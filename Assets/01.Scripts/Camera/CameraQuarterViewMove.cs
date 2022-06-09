using UnityEngine;

public class CameraQuarterViewMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 20;

    private Vector3 pos;

    private void Update()
    {
        float speed = this.speed;   

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 2;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -Vector3.forward * Time.unscaledDeltaTime * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.forward * Time.unscaledDeltaTime * speed;
        }
    
        if(Input.mouseScrollDelta.y != 0)
        {
            pos = transform.position + new Vector3(-Input.mouseScrollDelta.y*10, -Input.mouseScrollDelta.y*10, 0);
        }

        transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Clamp(pos.x, 50, 90), Mathf.Clamp(pos.y, 50, 90), Mathf.Clamp(transform.position.z, -20, 20)), Time.deltaTime*5);
    }
}