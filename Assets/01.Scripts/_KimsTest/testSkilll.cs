using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testSkilll : MonoBehaviour
{
    private Button turretPosBtn;

    [SerializeField]
    private int count;
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            testScriptts.Instance.turPos = count;
            Debug.Log(count);
            testScriptts.Instance.Create();
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
