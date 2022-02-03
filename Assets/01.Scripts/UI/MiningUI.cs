using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiningUI : MonoBehaviour
{
    private Image mineImg;

    private Player player;

    [SerializeField]
    public int myCount;

    void Start()
    {
        mineImg = GetComponent<Image>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        MinerImg();
    }

    public void MinerImg()
    {
        float dis = Vector3.Distance(this.gameObject.transform.position, player.gameObject.transform.position);
        mineImg.color = new Color(1, 1, 1, dis / 10);
        mineImg.sprite = TrainManager.instance.mine[myCount].img;
    }
}
