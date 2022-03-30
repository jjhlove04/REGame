using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeBtn : MonoBehaviour
{
    public int myCount;

    private bool isUpgrade = false;

    public Button nextBtn;
    public Button nextLineBtn;
    public GameObject floor;

    private void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            if(floor != null)
            {
                foreach (var item in floor.GetComponentsInChildren<Button>())
                {
                    item.interactable = false;
                    isUpgrade = false;
                }
            }
            this.gameObject.GetComponent<Button>().interactable = false;
            isUpgrade = true;
        });

        if (myCount == 1)
        {
            this.gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            this.gameObject.GetComponent<Button>().interactable = false;
        }

    }

    private void Update()
    {
        if(isUpgrade)
        {
            if(nextBtn == null)
            {
                return;
            }

            nextBtn.interactable = true;

            if (nextLineBtn != null)
            {
                nextLineBtn.interactable = true;
            }
            isUpgrade = false;
        }
    }
}
