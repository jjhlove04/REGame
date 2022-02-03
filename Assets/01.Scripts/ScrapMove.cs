using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapMove : MonoBehaviour
{
    public float speed = 3f;
    private Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }
    }
}