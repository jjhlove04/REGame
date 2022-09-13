using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleUII : MonoBehaviour
{
    private static TitleUII _ui = new TitleUII();
    public static TitleUII UI { get { return _ui; } }

    public Button[] buyBtns;

    [Header("���׷��̵� ����")]
    public Button[] upGradeBtns; //0�� �ͷ�, 1�� ����, 2�� Ÿ��
    public GameObject[] upGradePanels; //0�� �ͷ�, 1�� ����, 2�� Ÿ��

    [Header("����غ� �г� ����")]
    Sequence openSequence;
    Sequence closeSequence;
    [SerializeField] private RectTransform mapPanel;
    [SerializeField] private RectTransform itemPanel;
    [SerializeField] private RectTransform turretPanel;
    public Button startBtn;


    [Space(30)]
    [SerializeField] private Text repairCost;
    [SerializeField] private Text towingCost;

    public int curExp = 0;
    public int maxExp = 30;
    public Slider expBar;
    public Text levelTxt;
    //public Text techPointTxt;
    //public Text goldTxt;

    private void Awake()
    {

        openSequence = DOTween.Sequence();
        closeSequence = DOTween.Sequence();
        _ui = this;

        //�г��� Ȯ��
        //checkPanel.transform.DOScale(new Vector3(1,1,1),0.8f)
        //nickCheckBtn.onClick.AddListener(()=> RegisterDataConnect());

        startBtn.onClick.AddListener(() =>
        {
            LoadingSceneUI.LoadScene("Main");
        });
        upGradeBtns[0].onClick.AddListener(() =>
        {
            RemoveBtn();
            upGradePanels[0].SetActive(true);
            TitleMoveScript.indexNum = 4;
        });
        upGradeBtns[2].onClick.AddListener(() =>
        {
            RemoveBtn();
            upGradePanels[2].SetActive(true);
            TitleMoveScript.indexNum = 4;
        });


    }
    private void Update()
    {
        Update_MousePosition();
    }

    //�г� ���� �Լ�
    public void RemoveBtn()
    {
        for (int i = 0; i < 3; i++)
        {
            upGradeBtns[i].gameObject.SetActive(false);
        }
    }

    private void Update_MousePosition()
    {
        Vector2 mousePos = Input.mousePosition;
        //{"Wav {0}"}
    }

    public void ReadySetUpPanel(int num)
    {
        openSequence.SetAutoKill(true);
        closeSequence.SetAutoKill(true);
        if (num == 1)
        {
            openSequence.Kill();
            openSequence.Append(mapPanel.DOAnchorPosX(49, 1.2f).SetEase(Ease.InOutExpo));
            openSequence.Append(turretPanel.DOAnchorPosX(362, 1.2f).SetEase(Ease.InOutExpo));
            openSequence.Append(turretPanel.DOAnchorPosY(291, 1.2f).SetEase(Ease.InOutExpo));
            openSequence.Append(itemPanel.DOAnchorPosX(362, 1.2f).SetEase(Ease.InOutExpo));
            openSequence.Append(itemPanel.DOAnchorPosY(-156, 1.2f).SetEase(Ease.InOutExpo));
        }
        if (num == 2)
        {
            closeSequence.Kill();
            closeSequence.Append(mapPanel.DOAnchorPosX(-727, 0.6f).SetEase(Ease.InOutExpo));
            closeSequence.Append(turretPanel.DOAnchorPosX(1253, 0.6f).SetEase(Ease.InOutExpo));
            closeSequence.Append(turretPanel.DOAnchorPosY(768, 0.6f).SetEase(Ease.InOutExpo));
            closeSequence.Append(itemPanel.DOAnchorPosX(1253, 0.6f).SetEase(Ease.InOutExpo));
            closeSequence.Append(itemPanel.DOAnchorPosY(-768, 0.6f).SetEase(Ease.InOutExpo));

        }

    }


    private void ExpBar()
    {
        expBar.value = Mathf.Lerp(expBar.value, (float)curExp / (float)maxExp, Time.deltaTime * (2 + (curExp / 500)));

        if (expBar.value >= 0.99f)
        {
            if (curExp >= maxExp)
            {
                //TestTurretDataBase.Instance.curTp++;
                TestTurretDataBase.Instance.level++;
                curExp = curExp - maxExp;
                if (TestTurretDataBase.Instance.level % 20 == 0)
                {
                    maxExp = (int)(((maxExp + (TestTurretDataBase.Instance.level + (TestTurretDataBase.Instance.level - 1))) * (TestTurretDataBase.Instance.level / (TestTurretDataBase.Instance.level - 1)) + maxExp) * 1.2f);
                }
                else
                {
                    maxExp = (maxExp + (TestTurretDataBase.Instance.level + (TestTurretDataBase.Instance.level - 1))) * (TestTurretDataBase.Instance.level / (TestTurretDataBase.Instance.level - 1)) + maxExp;
                }
                expBar.value = 0;
                levelTxt.text = TestTurretDataBase.Instance.level.ToString();
            }
        }
    }
}
