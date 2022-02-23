using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    private float size;

    public float speed = 1;

    [SerializeField]
    private GameObject station;    
    [SerializeField]
    private GameObject station2;


    private void Start()
    {
        size = transform.GetChild(0).localScale.z;
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
                if (-(size * 1f) >= transform.GetChild(i).position.z)
                {
                    SwapeBackground(transform.GetChild(i));
                }
            }
        }



        if(GameManager.Instance.state == GameManager.State.End)
        {
            station.SetActive(true);
            station2.SetActive(true);
        }
    }

    private void MoveBackground(Transform transform)
    {
        transform.position -= new Vector3(0, 0, speed);
    }

    private void SwapeBackground(Transform transform)
    {
        transform.position += new Vector3(0, 0, size*2);
    }
}