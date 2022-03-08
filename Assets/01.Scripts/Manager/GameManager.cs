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
                state = State.End;
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
                break;
            case State.End:
                SceneManager.LoadScene("Station");
                state = State.Ready;
                break;
            default:
                break;
        }
    }
}
