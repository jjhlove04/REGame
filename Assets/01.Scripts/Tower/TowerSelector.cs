using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject[] background;

    [SerializeField]
    private GameObject tower;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            foreach (var back in background)
            {
                back.gameObject.SetActive(false);
            }

            transform.parent.Find("Background").gameObject.SetActive(true);

            TowerManager.Instance.SelectTower(tower);
        });
    }
}
