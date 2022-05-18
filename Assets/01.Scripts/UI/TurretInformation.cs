using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TurretInformation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TitleUI titleUI;
    public string information;
    // Start is called before the first frame update
    void Start()
    {
        titleUI = TitleUI.UI;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 mousePos = Input.mousePosition;

        titleUI.informationPanel.gameObject.SetActive(true);
        titleUI.informationPanel.transform.GetChild(0).GetComponent<Text>().text = information;
        titleUI.informationPanel.transform.position = mousePos;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        titleUI.informationPanel.gameObject.SetActive(false);
    }
}
