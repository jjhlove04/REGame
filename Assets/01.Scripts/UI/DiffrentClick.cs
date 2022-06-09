using UnityEngine;
using UnityEngine.EventSystems;

public class DiffrentClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            testScriptts.Instance.Speeeeeed();
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            testScriptts.Instance.Minuspeed();
        }
    }
}
