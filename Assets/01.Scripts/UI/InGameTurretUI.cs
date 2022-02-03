using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameTurretUI : MonoBehaviour
{
    public float dist = 5;
    private Player player;
    public bool onTurret = false;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(this.gameObject.transform.position, player.transform.position);

        if (dis <= dist && !onTurret)
        {
            this.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            this.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }
}
