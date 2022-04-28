using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public GameObject ammoParent;
    [HideInInspector]
    public bool isAmmo;
    [SerializeField]
    private float resizeAmount;

    public void ResizeAmmo()
    {
        if (gameObject.transform.childCount % 2 == 0)
        {
            if (isAmmo)
            {
                gameObject.GetComponent<GridLayoutGroup>().cellSize
                    = new Vector2(
                        gameObject.GetComponent<GridLayoutGroup>().cellSize.x / resizeAmount,
                        gameObject.GetComponent<GridLayoutGroup>().cellSize.y / resizeAmount);
                gameObject.GetComponent<GridLayoutGroup>().spacing
                    = new Vector2(
                        gameObject.GetComponent<GridLayoutGroup>().spacing.x / resizeAmount, 0);
                isAmmo = false;
            }
            else
            {
                return;
            }
        }
    }
}
