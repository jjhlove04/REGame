using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    private float size;

    public float speed = 1;

    public GameObject[] terrain;
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

                if (-(size * 1f) >= transform.GetChild(i).position.z)
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

    public void ChangeBackground(int round)
    {
        if(round % 100 == 0)
        {
            Debug.Log("0ตส");
            for (int i = 0; i < terrain.Length; i++)
            {
                terrain[i].SetActive(false);
            }

            switch (round)
            {
                case 100:
                    terrain[0].SetActive(true);
                    terrain[1].SetActive(true);
                    terrain[2].SetActive(true);
                    break;

                case 200:
                    terrain[3].SetActive(true);
                    terrain[4].SetActive(true);
                    terrain[5].SetActive(true);
                    break;

                case 300:
                    terrain[6].SetActive(true);
                    terrain[7].SetActive(true);
                    terrain[8].SetActive(true);
                    break;

                case 400:
                    terrain[9].SetActive(true);
                    terrain[10].SetActive(true);
                    terrain[11].SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
}