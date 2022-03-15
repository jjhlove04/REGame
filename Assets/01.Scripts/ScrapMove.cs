using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapMove : MonoBehaviour
{
    public float speed = 3f;
    private GearScript gearEnd;
    
    void Start()
    {
        gearEnd = FindObjectOfType<GearScript>();
    }

    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, gearEnd.gameObject.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
            UIManager.UI.scrapAmount += 1;
            UIManager.UI.CheckScrapAmount();
        }
    }
}