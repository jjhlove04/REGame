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
    private void Update()
    {
        if (titleUI.informationPanel.gameObject.activeSelf == true)
        {
            Vector3 mousePos = Input.mousePosition;

            titleUI.informationPanel.transform.position = mousePos;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        titleUI.informationPanel.gameObject.SetActive(true);
        titleUI.informationPanel.transform.GetChild(0).GetComponent<Text>().text = information;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        titleUI.informationPanel.gameObject.SetActive(false);
    }
}
