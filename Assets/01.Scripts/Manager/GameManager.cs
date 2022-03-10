using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
    }

    public enum State
    {
        Ready,
        Play,
        Stop,
        End
    }

    public State state;

    public float remainingTime;
    public float stageTime;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        remainingTime = stageTime;
    }

    private void OnDestroy()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        CurState();
        if (state == State.Play)
        {
            remainingTime -= Time.deltaTime;
            if(remainingTime <= 0)
            {
                state = State.Stop;
            }

            if(remainingTime <= 10)
            {
                GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < enemy.Length; i++)
                {
                    enemy[i].transform.position = new Vector3(enemy[i].transform.position.x, enemy[i].transform.position.y, (enemy[i].transform.position.z) - (Time.deltaTime * 6));
                }
            }

            if(TrainScript.instance.curTrainHp <= 0)
            {
                state = State.End;
            }
        }
    }
    
    private void CurState()
    {
        switch (state)
        {
            case State.Ready:
                break;
            case State.Play:
                break;
            case State.Stop:
                LoadingSceneUI.LoadScene("Station");
                state = State.Ready;
                break;
            case State.End:
                LoadingSceneUI.LoadScene("Station");
                state = State.Ready;
                break;
            default:
                break;
        }
    }
}
