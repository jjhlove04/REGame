using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public enum Kind
    {
        GAMESTOP,
        RESUME,
        CHECK,
        RESTART,
        SETTING,
        EXIT,
        GAMEOVER,
        CHECKEXIT,
        SETTINGEXIT,
        GAMESETTINGEXIT
    }
    public Kind kind;

    public GameObject stopButton;
    public GameObject menuObj=null;
    public GameObject settingObj;
    public GameObject gameOver;
    public GameObject checkObj;

    public string sceneName;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        switch (kind)
        {
            case Kind.GAMESTOP:
                button.onClick.AddListener(GameStopButton);
                break;
            case Kind.RESUME:
                button.onClick.AddListener(Resume);
                break;
            case Kind.CHECK:
                button.onClick.AddListener(ReallyCheck);
                break;
            case Kind.RESTART:
                button.onClick.AddListener(Restart);
                break;
            case Kind.SETTING:
                button.onClick.AddListener(Setting);
                break;
            case Kind.CHECKEXIT:
                button.onClick.AddListener(ReallyExit);
                break;
            case Kind.EXIT:
                button.onClick.AddListener(Exit);
                break;
            case Kind.SETTINGEXIT:
                button.onClick.AddListener(SettingExit);
                break;
        }
    }
    //게임 씬에서 게임 멈춤 버튼
    public void GameStopButton()
    {
        Time.timeScale = 0;
        GameManager.Instance.state = GameManager.State.Stop;
        menuObj.SetActive(true);
        gameObject.SetActive(false);
    }

    //게임 멈춤에서 계속하기 버튼
    public void Resume()
    {
        Time.timeScale = 1;
        GameManager.Instance.state = GameManager.State.Play;

        stopButton.SetActive(true);
        menuObj.SetActive(false);
    }

    public void ReallyCheck()
    {
        checkObj.SetActive(true);
    }

    public void ReallyExit()
    {
        checkObj.SetActive(false);
    }

    //게임 다시시작
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    //게임 세팅
    public void Setting()
    {
        settingObj.SetActive(true);
    }

    //메인 화면으로 나가기
    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

    //세팅 나가기 및 볼륨 저장
    public void SettingExit()
    {
        Time.timeScale = 1;
        GameManager.Instance.state = GameManager.State.Play;
        settingObj.SetActive(false);
    }
}
