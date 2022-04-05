using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    private float size;

    public float speed = 1;


    private void Start()
    {
        size =  200;
    }


    private void Update()
    {
        if (GameManager.Instance.state != GameManager.State.Stop)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                MoveBackground(transform.GetChild(i));
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                if (-(size * 2f) >= transform.GetChild(i).position.z)
                {
                    SwapeBackground(transform.GetChild(i));
                }
            }
        }
    }

    private void MoveBackground(Transform transform)
    {
        transform.position -= new Vector3(0, 0, speed * GameManager.Instance.gameSpeed);
    }

    private void SwapeBackground(Transform transform)
    {
        transform.position += new Vector3(0, 0, size*3f);
    }
}