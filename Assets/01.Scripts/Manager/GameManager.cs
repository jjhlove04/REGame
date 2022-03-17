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

    [SerializeField]
    private float gameTime = 0f;
    public float gameSpeed = 1f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        DontDestroyOnLoad(this);
    }
    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        gameTime = Time.unscaledTime;
        CurState();
        gameTime++;
        if (state == State.Play)
        {
            Time.timeScale = gameSpeed;
            if (TrainScript.instance.curTrainHp <= 0)
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
                LoadingSceneUI.LoadScene("Station");
                state = State.Ready;
                break;
            default:
                break;
        }
    }
}
