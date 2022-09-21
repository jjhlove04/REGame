using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour
{

    public Button[] commonItem;
    public GameObject detailPanel;
    //public Button closeBtn;
    public Text itemNameText;
    public Text rankText;
    public Text effectiveText;
    public bool isOnPop = false;

    public GameObject itemCard;
    public Image itemImg;
    public Text itmeName;
    public Button enterBtn;

    private int reCount = 0;

    public GameObject messagePrefabs;
    public GameObject messagePrents;

    private void Awake() {
        //detailPanel.transform.localScale = Vector3.zero;
        //closeBtn.onClick.AddListener(CloseFunc);
    }

    private void Start()
    {
        
        enterBtn.interactable = false;
        itemCard.transform.localScale = Vector3.zero;
        for (int i = 0; i < this.commonItem.Length; i++)
        {
            int itemIndex = i;

            commonItem[i].interactable = TestTurretDataBase.Instance.postItemDic[commonItem[i].ToString()];
            commonItem[i].onClick.AddListener(()=> this.CommonCollection(itemIndex));

        }
    }

    private void Update()
    {

        for (int i = 0; i < commonItem.Length; i++)
        {
            if (commonItem[i].interactable == false)
            {
                commonItem[i].GetComponent<Image>().color = new Color(0, 0, 0, 1);
            }
            else
            {
                commonItem[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    

    public void ShowMessage(string message)
    {
        GameObject ms = Instantiate(messagePrefabs, transform);
        ms.GetComponent<Text>().text = message;
        ms.GetComponent<Text>().DOFade(0,4).OnComplete(()=> Destroy(ms));
        ms.transform.SetParent(messagePrents.transform, false);
    }
    public void CommonCollection(int index){
        detailPanel.transform.DOScale(new Vector3(1,1,1),0.5f);
        itemNameText.text = string.Format("-아이템 이름 : <color=#A4A4A4>{0}</color>",ItemDictionary._instance.itemContainerCom[index].Item1);
        effectiveText.text = string.Format("-효과 : {0}", ItemDictionary._instance.itemContainerCom[index].Item2);
        rankText.text = string.Format("-등급 : <color=#6E85B7>{0}</color>",ItemDictionary._instance.itemContainerCom[index].Item3);

        isOnPop = true;
    }

    public void CloseFunc()
    {
        detailPanel.transform.DOScale(Vector3.zero, 0.5f);
        isOnPop = false;
    }

    public void Gatcha()
    {
        int i = Random.Range(0, commonItem.Length);

        if (reCount < 21)
        {
            Randand(i);

            reCount++;

        }
        else
        {
            ShowMessage(string.Format($"얻을 수 있는 아이템을 모두 획득 하였습니다."));
        }

    }

    //새로만든 뽑기 시스템
    public void CardSpin()
    {
        if(TestTurretDataBase.Instance.curTp > 0)
        {
            itemCard.transform.DORotate(new Vector3(0,3600,0), 2, RotateMode.FastBeyond360).SetEase(Ease.OutCubic).OnComplete(() => enterBtn.interactable = true);
            itemCard.transform.DOScale(new Vector3(1,1,1),2);
            itemImg.DOFade(1,2);
            Gatcha();
            TestTurretDataBase.Instance.curTp--;
        }
        else
        {
            ShowMessage($"<color=red>소지하고 있는 T.P가 부족합니다.</color> ");
        }

    }
    public void CardOff()
    {
        itemCard.transform.DOScale(new Vector3(0,0,0), 0.2f);
        
    }

    public int Randand(int i)
    {
        
        if (commonItem[i].interactable == false)
        {
            commonItem[i].interactable = true;
            itemImg.sprite = ItemDictionary._instance.itemContainerCom[i].Item4; //이미지
            itmeName.text = ItemDictionary._instance.itemContainerCom[i].Item1; //텍스트 이름
            ShowMessage(string.Format("소환에서 {0} <{1}> 획득",ItemDictionary._instance.itemContainerCom[i].Item1, ItemDictionary._instance.itemContainerCom[i].Item3));

            TestTurretDataBase.Instance.postItemDic[commonItem[i].ToString()] = commonItem[i].interactable;
            TestTurretDataBase.Instance.postItemObj.Add(commonItem[i].GetComponent<TitleItemBase>().obj.GetComponent<TrainItem>());
            return 0;
        }
        else
        {
            int j = Random.Range(0, commonItem.Length);

            Randand(j);
        }

        return 0;
    }
}
