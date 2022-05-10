using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerr : MonoBehaviour
{
    private static GameManagerr instance;

    public static GameManagerr Instance
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

    public int turretPtice = 10;

    public int goldAmount = 0;
    public int expAmount = 0;
    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    private void OnDestroy()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        gameTime = Time.unscaledTime;
        gameTime++;
        if (state == State.Play)
        {
            Time.timeScale = gameSpeed;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            state = State.End;
        }
    }
}