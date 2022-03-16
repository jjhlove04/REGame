using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testScriptts : MonoBehaviour
{
    public Button speedBtn;
    public Text speedTxt;
    private int speedBtnCount;

    public Button NextWaveBtn;

    // Start is called before the first frame update
    void Start()
    {
        speedBtn.onClick.AddListener(ChangeSpeed);
        NextWaveBtn.onClick.AddListener(NextWave);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeSpeed()
    {
        speedBtnCount++;
        switch (speedBtnCount)
        {
            case 0:
                GameManager.Instance.gameSpeed = 1f;
                speedTxt.text = GameManager.Instance.gameSpeed + "X";
                break;
            case 1:
                GameManager.Instance.gameSpeed = 1.5f;
                speedTxt.text = GameManager.Instance.gameSpeed + "X";
                break;
            case 2:
                GameManager.Instance.gameSpeed = 2f;
                speedTxt.text = GameManager.Instance.gameSpeed + "X";
                break;
            case 3:
                GameManager.Instance.gameSpeed = 4f;
                speedTxt.text = GameManager.Instance.gameSpeed + "X";
                speedBtnCount = -1;
                break;
            default:
                break;
        }
    }

    public void NextWave()
    {
        SpawnMananger.Instance.curTime = SpawnMananger.Instance.roundCurTime;
    }
}
