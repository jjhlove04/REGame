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

    public int goldAmount = 0;
    public int expAmount = 0;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        //Application.targetFrameRate = 60;
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
        gameTime++;
        if (state == State.Play)
        {
            Time.timeScale = gameSpeed;
        }

    }
}
