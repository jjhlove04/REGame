using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FeedMessageManager : MonoBehaviour
{
    public static FeedMessageManager instance;
    
    public GameObject messagePrefabs;
    public static FeedMessageManager MyInstance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<FeedMessageManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ShowMessage(string message)
    {
        Debug.Log(message);
        GameObject ms = Instantiate(messagePrefabs, transform);
        ms.GetComponent<Text>().text = message;
        ms.GetComponent<Text>().DOFade(0,4).OnComplete(()=> Destroy(ms));
    }
}
