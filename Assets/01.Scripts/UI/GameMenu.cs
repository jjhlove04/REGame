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
    //���� ������ ���� ���� ��ư
    public void GameStopButton()
    {
        Time.timeScale = 0;
        GameManager.Instance.state = GameManager.State.Stop;
        menuObj.SetActive(true);
        gameObject.SetActive(false);
    }

    //���� ���㿡�� ����ϱ� ��ư
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

    //���� �ٽý���
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    //���� ����
    public void Setting()
    {
        settingObj.SetActive(true);
    }

    //���� ȭ������ ������
    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

    //���� ������ �� ���� ����
    public void SettingExit()
    {
        Time.timeScale = 1;
        GameManager.Instance.state = GameManager.State.Play;
        settingObj.SetActive(false);
    }
}
